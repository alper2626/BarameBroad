using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Extensions.UIExtensions.ControllerExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Module.Auth.Business.Abstract;
using Module.Auth.Entities.GetAdmin;
using Module.Auth.Entities.PostAdmin;

namespace Module.Auth.UI.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class AccountController : Controller
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(PostLoginModel model)
        {
            var res = _userService.Login(model);
            if (!res.Success)
                return View().Error(res.Message);

            await LoginUser((GetLoggedUserWithClaimModel)res.Data);
            return RedirectToAction("Index", "Home", new { Area = "Admin" }).Success(res.Message);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account", new { Area = "Auth" }).Success("Çıkış Yapıldı.");
        }

        private async Task LoginUser(GetLoggedUserWithClaimModel user)
        {
            await LogOut();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.PrimarySid,user.Id.ToString()),
                new Claim(ClaimTypes.Role,string.Join(',',user.UserClaims.Select(r=>r.Name))),
                new Claim(ClaimTypes.Name,user.LoginName),
                new Claim("FirstName",user.FirstName),
                new Claim("LastName",user.LastName),
                new Claim("RegisterDate",user.CreateTime.ToString("yyyy-MM-dd HH:mm:ss,fff"))
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddHours(5) });
        }
    }

}
