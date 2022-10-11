using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BarameBroad.WebUI.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
