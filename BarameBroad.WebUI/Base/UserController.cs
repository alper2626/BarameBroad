using BaseEntities.Concrete;
using BaseEntities.Database;
using Core.DataAccess.Abstract;
using Core.ServiceInjector.Utilities.IoC;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace BarameBroad.WebUI.Base
{
    public class UserController<TBase> : Controller
        where TBase : Entity, new()
    {
        protected ICacheService CacheService;
        protected IEntityRepositoryBase<TBase> Repository;
        protected IHttpContextAccessor HttpContextAccessor;
        protected string UserLang;
        private IEntityRepositoryBase<ProgramEntity> repo;

        protected UserController(IEntityRepositoryBase<TBase> repo, ICacheService cacheService, IHttpContextAccessor httpContextAccessor)
        {
            Repository = repo;
            CacheService = cacheService;
            HttpContextAccessor = httpContextAccessor;
            if(HttpContextAccessor.HttpContext.Request.Cookies.TryGetValue(".AspNetCore.Culture", out string lang))
            {
                UserLang = lang.Split('|')[0].Split('=')[1];
            }
            else
            {
                UserLang = "tr-TR";
            }
        }

    }
}
