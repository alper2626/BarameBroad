using BaseEntities.EnumTypes;
using Core.Attributes;
using Core.DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;
using BaseEntities.Database;
using Microsoft.EntityFrameworkCore;
using Extensions.UIExtensions.ControllerExtensions;
using CrossCuttingConcerns.BaseControllers;
using BaseEntities.Response;
using DevExtreme.AspNet.Mvc;
using MemCaching.Abstract;

namespace BarameBroad.WebUI.Areas.SiteConfig.Controllers
{
    [Area("SiteConfig")]
    [InRole(UserRole.Admin)]
    public class UserCommentController : Controller
    {
        private IEntityRepositoryBase<BaseEntities.Database.UserComment> _userComment;
        IQueryableRepositoryBase<BaseEntities.Database.UserComment> _userCommentQueryable;
        ICacheService _cacheService;

        public UserCommentController(IEntityRepositoryBase<BaseEntities.Database.UserComment> social, ICacheService cacheService, IQueryableRepositoryBase<BaseEntities.Database.UserComment> socialQueryable)
        {
            _userComment = social;
            _userCommentQueryable = socialQueryable;
            _cacheService = cacheService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GridSource(DataSourceLoadOptions loadOptions)
        {
            return Content(_userCommentQueryable.DxGridList<BaseEntities.Database.UserComment>(loadOptions, _userCommentQueryable.Table).ToString(), "application/json");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(Guid id)
        {
            return View(_userComment.Get(w => w.Id == id));
        }

        [HttpPost]
        public IActionResult Create(BaseEntities.Database.UserComment model)
        {
            var response = _userComment.SetStateEntity(model, EntityState.Added);
            if (response != null)
            {
                _cacheService.Delete("UserComments");
                return RedirectToAction("Index", "UserComment", new { area = "SiteConfig" }).Success(Constants.Success);
            }
                

            return View(model).Error(Constants.Error);


        }

        [HttpPost]
        public IActionResult Update(BaseEntities.Database.UserComment model)
        {
            var response = _userComment.SetStateEntity(model, EntityState.Modified);
            if (response != null)
            {
                _cacheService.Delete("UserComments");
                return RedirectToAction("Index", "UserComment", new { area = "SiteConfig" }).Success(Constants.Success);
            }
                

            return View(model).Error(Constants.Error);

        }

        public IActionResult Delete(Guid id)
        {
            if (_userComment.SetState(_userComment.Get(w => w.Id == id), EntityState.Deleted))
            {
                _cacheService.Delete("UserComments");
                return Json(new DataResponse { Success = true, Message = Constants.Success });
            }
                

            return Json(new DataResponse { Success = false, Message = Constants.Error });
        }
    }
}
