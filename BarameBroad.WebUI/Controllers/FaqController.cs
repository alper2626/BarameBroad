using BarameBroad.WebUI.Base;
using BaseEntities.Database;
using Core.DataAccess.Abstract;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BarameBroad.WebUI.Controllers
{
    [Route("Faq")]
    public class FaqController : UserController<Sss>
    {
        public FaqController(IEntityRepositoryBase<Sss> repo, ICacheService cacheService, IHttpContextAccessor httpContextAccessor) : base(repo, cacheService, httpContextAccessor)
        {
        }

        [Route("")]
        public IActionResult Index()
        {
            return View(Repository.GetList(w => w.Lang == UserLang).ToList());
        }
    }
}
