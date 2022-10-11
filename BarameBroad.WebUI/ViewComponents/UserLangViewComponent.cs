using BarameBroad.WebUI.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BarameBroad.WebUI.ViewComponents
{
    public class UserLangViewComponent : ViewComponent
    {
        readonly RequestLocalizationOptions _localizationOptions;
        public UserLangViewComponent(IOptions<RequestLocalizationOptions> localizationOptions, IHttpContextAccessor contextAccessor)
        {
            _localizationOptions = localizationOptions.Value;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            IRequestCultureFeature requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();

            var allCultures = _localizationOptions.SupportedCultures
                    .Select(culture => new UserCultureModel
                    {
                        Name = culture.Name,
                        Text = culture.DisplayName,
                        Active = requestCulture.RequestCulture.Culture.Name == culture.Name,
                    }).ToList();
            return View(allCultures);
        }
    }
}
