using BaseEntities.Concrete;
using BaseEntities.Database;
using Core.DataAccess.Abstract;
using Core.ServiceInjector.Utilities.IoC;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BarameBroad.WebUI.Base
{
    public class BaseController<TBase, TSub> : Controller
        where TBase : Entity, new()
        where TSub : LangEntity<TBase>, new()
    {
        protected ICacheService CacheService;
        protected IEntityRepositoryBase<TBase, TSub> Repository;

        protected BaseController(IEntityRepositoryBase<TBase, TSub> repo, ICacheService cacheService)
        {
            Repository = repo;
            CacheService = cacheService;
        }

    }
}
