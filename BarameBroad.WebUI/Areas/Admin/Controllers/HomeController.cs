using BaseEntities.EnumTypes;
using Core.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace BarameBroad.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [InRole(UserRole.Admin)]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(string message)
        {
            ViewData["Message"] = message;
            return View();
        }

    }

}
