using BaseEntities.Database;
using Core.DataAccess.Abstract;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BarameBroad.WebUI.ViewComponents
{
    public class UserHeaderViewComponent : ViewComponent
    {
        ICacheService _cacheService;
        IEntityRepositoryBase<MasterProgram> repo;
        IEntityRepositoryBase<MasterSiteConfig> _siteConfigRepo;
        IEntityRepositoryBase<SocialNetworkEntity> _socialRepo;
        string userLang;
        public UserHeaderViewComponent(ICacheService cacheService, IEntityRepositoryBase<MasterProgram> repo, IHttpContextAccessor contextAccessor, IEntityRepositoryBase<MasterSiteConfig> siteConfigRepo, IEntityRepositoryBase<SocialNetworkEntity> socialRepo)
        {
            _cacheService = cacheService;
            this.repo = repo;
            _siteConfigRepo = siteConfigRepo;
            _socialRepo = socialRepo;

            if (contextAccessor.HttpContext.Request.Cookies.TryGetValue(".AspNetCore.Culture", out string lang))
            {
                userLang = lang.Split('|')[0].Split('=')[1];
            }
            else
            {
                userLang = "tr-TR";
            }

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var res = _cacheService.Get<List<MasterProgram>>("BaseServices");

            if (res == null)
            {
                res = repo.GetList(w => w.MasterProgramId == null || w.MasterProgramId == Guid.Empty, new System.Linq.Expressions.Expression<Func<MasterProgram, object>>[] { w => w.Childrens }).ToList();
                _cacheService.AddOrUpdate("BaseServices", res);
            }


            var siteConfig = _cacheService.Get<MasterSiteConfig>("SiteConfig");

            if (siteConfig == null)
            {
                siteConfig = _siteConfigRepo.First(new System.Linq.Expressions.Expression<Func<MasterSiteConfig, object>>[] { w => w.Childrens });
                _cacheService.AddOrUpdate("SiteConfig", siteConfig);
            }

            ViewData["Config"] = siteConfig.Childrens.First(w => w.Lang == userLang);


            var socials = _cacheService.Get<List<SocialNetworkEntity>>("SocialNetworks");

            if (socials == null)
            {
                socials = _socialRepo.GetList().ToList();
                _cacheService.AddOrUpdate("SocialNetworks", socials);
            }

            ViewData["Socials"] = socials;
            var programs = res.OrderBy(w => w.Order).SelectMany(w => w.Childrens.Where(w => w.Lang == userLang)).ToList();
            return View(programs);
        }
    }
}
