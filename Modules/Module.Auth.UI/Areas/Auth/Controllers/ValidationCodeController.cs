using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Extensions.UIExtensions.ControllerExtensions;
using Microsoft.AspNetCore.Hosting;
using Module.Auth.Business.Abstract;
using Module.Auth.Entities.EnumTypes;
using Module.Auth.Entities.GetAdmin;
using Module.Auth.Entities.MailModel;
using Module.Auth.Entities.PostAdmin;
using Notification.Abstract;
using Notification.Entities.Concrete;

namespace Module.Auth.UI.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class ValidationCodeController : Controller
    {
        private IUserPasswordService _userPasswordService;
        private IUserService _userService;
        private IUserValidationCodeService _userValidationCodeService;
        private IMailingService _mailingService;

        public ValidationCodeController(IUserPasswordService userPasswordService, IUserService userService, IUserValidationCodeService userValidationCodeService, IMailingService mailingService)
        {
            _userPasswordService = userPasswordService;
            _userService = userService;
            _userValidationCodeService = userValidationCodeService;
            _mailingService = mailingService;
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string mailAddress)
        {
            if (string.IsNullOrEmpty(mailAddress) || !mailAddress.Contains("@"))
                return View().Error("Lütfen Mail Adresinizi Girin");

            var res = _userService.UserFromMailAddress(mailAddress);
            if (!res.Success)
                return View().Error(res.Message);
            var userData = (GetUserModel)res.Data;
            var codeRes = _userValidationCodeService.Create(
                new PostUserValidationCodeCreateModel(TimeSpan.FromMinutes(30))
                {
                    ValidationCodeType = ValidationCodeType.Password,
                    UserId = userData.Id
                });
            if (!codeRes.Success)
                return View().Error(codeRes.Message);
            var urlBase =
                $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            ResetPasswordMailModel mailModel = new ResetPasswordMailModel
            {
                Code = (string) codeRes.Data,
                GetUserModel = userData,
                DirectUrl = urlBase + Url.Action("ValidatePasswordCodeParam", "ValidationCode",
                    new {area = "Auth", code = (string) codeRes.Data, mailAddress = userData.MailAddress}),
            };
            SendPasswordCode(mailModel);

            TempData["UserMailAddress"] = mailAddress;
            return RedirectToAction("ValidatePasswordCode", "ValidationCode", new { area = "Auth" });
        }

        public IActionResult ValidatePasswordCode()
        {
            PostResetPasswordCodeModel viewModel = new PostResetPasswordCodeModel();
            if (TempData["UserMailAddress"] != null)
                viewModel.MailAddress = TempData["UserMailAddress"].ToString();

            return View(viewModel);
        }

        public IActionResult ValidatePasswordCodeParam(string mailAddress,string code)
        {

            PostResetPasswordCodeModel viewModel = new PostResetPasswordCodeModel
            {
                MailAddress = mailAddress,
                Code = code
            };

            return View("ValidatePasswordCode", viewModel);
        }

        [HttpPost]
        public IActionResult ValidatePasswordCode(PostResetPasswordCodeModel model)
        {
            var res = _userService.UserFromMailAddress(model.MailAddress);
            if (!res.Success)
                return View().Error(res.Message);

            var userModel = (GetUserModel)res.Data;
            model.UserId = userModel.Id;
            model.PostUserPasswordUpdateModel.UserId = userModel.Id;
            var codeRes = _userValidationCodeService.ValidateAndSetUsed(model);
            if (!codeRes.Success)
            {
                TempData["UserMailAddress"] = model.MailAddress;
                return RedirectToAction("ValidatePasswordCode", "ValidationCode",new{area="Auth"}).Error(codeRes.Message);
            }

            var passRes = _userPasswordService.UpdateWithoutValidate(model.PostUserPasswordUpdateModel);
            if (passRes.Success)
            {
                SendPasswordChanged(new ChangedPasswordMailModel
                {
                    GetUserModel = userModel
                });
                return RedirectToAction("Login", "Account", new { area = "Auth" }).Success(codeRes.Message);
            }

            TempData["UserMailAddress"] = model.MailAddress;
            return RedirectToAction("ValidatePasswordCode", "ValidationCode", new { area = "Auth" }).Error(passRes.Message);

        }

        private void SendPasswordCode(ResetPasswordMailModel model)
        {
            Task.Run(() =>
            {
                _mailingService.Send(new SendMailBaseModel
                {
                    To = new HashSet<string>
                    {
                        model.GetUserModel.MailAddress
                    },
                    Subject = "Şifre Sıfırlama Talebi !!!",
                    MailModel = model,
                    IsContactMail = false,
                    TemplateName = "_ResetPasswordMail"
                });
            });
        }

        private void SendPasswordChanged(ChangedPasswordMailModel model)
        {
            Task.Run(() =>
            {
                _mailingService.Send(new SendMailBaseModel
                {
                    To = new HashSet<string>
                    {
                        model.GetUserModel.MailAddress
                    },
                    Subject = "Şifreniz Değiştirildi !!!",
                    MailModel = model,
                    IsContactMail = false,
                    TemplateName = "_ChangedPasswordMail"
                });
            });
        }

    }
}
