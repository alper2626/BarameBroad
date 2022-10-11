using Microsoft.AspNetCore.Mvc;
using System;
using BaseEntities.EnumTypes;
using Core.Attributes;
using DevExtreme.AspNet.Mvc;
using Extensions.UIExtensions.ControllerExtensions;
using Module.Auth.Business.Abstract;
using Module.Auth.Entities.GetAdmin;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.UI.Areas.Auth.Controllers
{
    [Area("Auth")]
    [InRole(UserRole.Admin)]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult GridSource(DataSourceLoadOptions loadOptions)
        {
            var response = _userService.GridSource<GetUserModel>(loadOptions);
            return Content(response.Data.ToString(), "application/json");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(Guid id)
        {
            var response = _userService.GetById<PostUserUpdateModel>(id);
            if (response.Success)
                return View(response.Data);

            return RedirectToAction("Index", "User", new { Area = "Auth" }).Error(response.Message);
        }

        [HttpPost]
        public IActionResult Create(PostUserCreateModel model)
        {
            var response = _userService.Create(model);

            if (response.Success)
                return RedirectToAction("Update", "User", new { Area = "Auth",id=response.Data }).Success(response.Message);

            TempData["Error"] = response.Message;
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(PostUserUpdateModel model)
        {
            var response = _userService.Update(model);
            if (response.Success)
                return RedirectToAction("Index", "User", new { Area = "Auth" }).Success(response.Message);

            TempData["Error"] = response.Message;
            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            var response = _userService.Delete(id);
            return Json(response);
        }
    }
}
