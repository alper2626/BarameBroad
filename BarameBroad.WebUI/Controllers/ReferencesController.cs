using BaseEntities.Database;
using Core.DataAccess.Abstract;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BarameBroad.WebUI.Controllers
{
    [Route("referans")]
    public class ReferencesController : Controller
    {
        private IEntityRepositoryBase<Reference> _reference;
        ICacheService _cacheService;

        public ReferencesController(IEntityRepositoryBase<Reference> reference, ICacheService cacheService)
        {
            _reference = reference;
            _cacheService = cacheService;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View(_reference.GetList().ToList());
        }

        [Route("anasayfa")]
        public IActionResult UserCommentsPartial()
        {
            var res = _cacheService.Get<List<Reference>>("References");

            if (res == null)
            {
                res = _reference.GetListSkipTake(0,6).ToList();
                _cacheService.AddOrUpdate("References", res);
            }

            return PartialView("Partials/_ReferencesPartial", res);
        }
    }
}
