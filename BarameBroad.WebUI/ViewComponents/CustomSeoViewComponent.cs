using System.Threading.Tasks;
using BaseEntities.Database;
using Core.DataAccess.Abstract;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BarameBroad.WebUI.ViewComponents
{
    [ViewComponent]
    public class CustomSeoViewComponent : ViewComponent
    {
        ICacheService _cacheService;
        IEntityRepositoryBase<MasterSiteConfig> _siteConfigRepo;
        string userLang;
        public CustomSeoViewComponent(IEntityRepositoryBase<MasterSiteConfig> siteConfigRepo, ICacheService cacheService, IHttpContextAccessor contextAccessor)
        {
            _siteConfigRepo = siteConfigRepo;
            _cacheService = cacheService;
            if (contextAccessor.HttpContext.Request.Cookies.TryGetValue(".AspNetCore.Culture", out string lang))
            {
                userLang = lang.Split('|')[0].Split('=')[1];
            }
            else
            {
                userLang = "tr-TR";
            }
        }

        public async Task<IViewComponentResult> InvokeAsync(SeoEntity model)
        {
            var siteConfig = _cacheService.Get<MasterSiteConfig>("SiteConfig");

            if (siteConfig == null)
            {
                siteConfig = _siteConfigRepo.First(new System.Linq.Expressions.Expression<Func<MasterSiteConfig, object>>[] { w => w.Childrens });
                _cacheService.AddOrUpdate("SiteConfig", siteConfig);
            }

            ViewData["ConfigData"] = siteConfig.Childrens.First(w => w.Lang == userLang);
            if (model == null)
                model = new SeoEntity { Description = "", Keywords = "", Lang = "tr-TR", Title = "" };

            return View(model);
        }

    }
}
