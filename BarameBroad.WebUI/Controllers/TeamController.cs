using BarameBroad.WebUI.Base;
using BaseEntities.Database;
using Core.DataAccess.Abstract;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BarameBroad.WebUI.Controllers
{
    [Route("Takim")]
    public class TeamController : UserController<TeamEntity>
    {
        public TeamController(IEntityRepositoryBase<TeamEntity> repo, ICacheService cacheService, IHttpContextAccessor httpContextAccessor) : base(repo, cacheService, httpContextAccessor)
        {
        }

        [Route("")]
        public IActionResult Index()
        {
            return View(Repository.GetList(w => w.Lang == UserLang, new System.Linq.Expressions.Expression<Func<TeamEntity, object>>[] { w => w.Master }));
        }
    }
}
