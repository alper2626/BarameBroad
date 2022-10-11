using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BarameBroad.WebUI.ViewComponents
{
    public class AsideMenuAdminViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
