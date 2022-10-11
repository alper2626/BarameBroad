using System;
using System.Threading.Tasks;
using BaseEntities.Database;
using Core.DataAccess.Abstract;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BarameBroad.WebUI.ViewComponents
{
    [ViewComponent]
    public class SeoViewComponent : ViewComponent
    {
        ICacheService _cacheService;
        IEntityRepositoryBase<MasterSeoEntity> repo;
        IEntityRepositoryBase<MasterSiteConfig> _siteConfigRepo;
        string userLang;
        public SeoViewComponent(IEntityRepositoryBase<MasterSeoEntity> seoService, IEntityRepositoryBase<MasterSiteConfig> siteConfigRepo, IHttpContextAccessor contextAccessor, ICacheService cacheService)
        {
            repo = seoService;
            _siteConfigRepo = siteConfigRepo;

            if (contextAccessor.HttpContext.Request.Cookies.TryGetValue(".AspNetCore.Culture", out string lang))
            {
                userLang = lang.Split('|')[0].Split('=')[1];
            }
            else
            {
                userLang = "tr-TR";
            }
            _cacheService = cacheService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid relatedId)
        {
            var siteConfig = _cacheService.Get<MasterSiteConfig>("SiteConfig");

            if (siteConfig == null)
            {
                siteConfig = _siteConfigRepo.First(new System.Linq.Expressions.Expression<Func<MasterSiteConfig, object>>[] { w => w.Childrens });
                _cacheService.AddOrUpdate("SiteConfig", siteConfig);
            }

            ViewData["ConfigData"] = siteConfig.Childrens.First(w => w.Lang == userLang);

            var res = repo.Get(w => w.RelatedId == relatedId,new System.Linq.Expressions.Expression<Func<MasterSeoEntity, object>>[] { w=>w.Childrens});
            if (res == null)
                res = new MasterSeoEntity
                {
                    RelatedId = relatedId,
                    Childrens = new List<SeoEntity>
                    {
                        new SeoEntity{ Description="", Keywords="",Lang="tr-TR",Title="" },
                        new SeoEntity{ Description="", Keywords="",Lang="en-US",Title="" }
                    }
                };
            return View(res.Childrens.First(w => w.Lang == userLang));
        }

    }
}
