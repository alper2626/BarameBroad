using System;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Mvc;
using BaseEntities.Concrete;
using BaseEntities.EnumTypes;
using Core.Attributes;
using Core.WebHelper;
using Extensions.UIExtensions.ControllerExtensions;
using Module.Auth.Business.Abstract;
using Module.Auth.Entities.EnumTypes;
using Module.Auth.Entities.GetAdmin;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.UI.Areas.Auth.Controllers
{
    [Area("Auth")]
    [InRole(UserRole.Admin)]
    public class PasswordController : Controller
    {
        private IUserPasswordService _userPasswordService;
        private IUserService _userService;
        private IUserValidationCodeService _userValidationCodeService;
        private IClaimWebHelper _claimWebHelper;

        public PasswordController(IUserPasswordService userPasswordService, IClaimWebHelper claimWebHelper, IUserService userService, IUserValidationCodeService userValidationCodeService)
        {
            _userPasswordService = userPasswordService;
            _claimWebHelper = claimWebHelper;
            _userService = userService;
            _userValidationCodeService = userValidationCodeService;
        }

        public IActionResult UpdatePasswordModal(SelectBoxModel model)
        {
            PostUserPasswordUpdateModel viewModel = new PostUserPasswordUpdateModel
            {
                UserId = model.Id,
                UserLoginName = model.DisplayValue
            };
            return PartialView("Partials/_UpdatePasswordModal", viewModel);
        }

        public IActionResult UpdatePasswordBySelf()
        {
            PostUserPasswordUpdateModel viewModel = new PostUserPasswordUpdateModel
            {
                UserId = _claimWebHelper.Id,
                UserLoginName = _claimWebHelper.FullName
            };
            return PartialView("Partials/_UpdatePasswordModal", viewModel);
        }

        public IActionResult Update(PostUserPasswordUpdateModel model)
        {
            if (_claimWebHelper.IsInSingleRole(UserRole.Admin))
                return Json(_userPasswordService.Update(model));

            return Json(_userPasswordService.Update(model));
        }

    }
}
