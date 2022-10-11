using BarameBroad.WebUI.Base;
using BarameBroad.WebUI.Models.FilterModel;
using BaseEntities.Database;
using BaseEntities.EnumTypes;
using Core.DataAccess.Abstract;
using Extensions.UIExtensions.ControllerExtensions;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BarameBroad.WebUI.Controllers
{
    [Route("sehirler")]
    public class CityController : UserController<BaseEntities.Database.City>
    {
        IQueryableRepositoryBase<BaseEntities.Database.City> _qRepo;
        IEntityRepositoryBase<ContentDetail> _detailRepo;
        public CityController(IEntityRepositoryBase<BaseEntities.Database.City> repo, ICacheService cacheService, IHttpContextAccessor httpContextAccessor, IQueryableRepositoryBase<BaseEntities.Database.City> qRepo, IEntityRepositoryBase<ContentDetail> detailRepo) : base(repo, cacheService, httpContextAccessor)
        {
            _qRepo = qRepo;
            _detailRepo = detailRepo;
        }

        [Route("sehir-detay/{friendlyName}/{detailfriendlyName?}")]
        public IActionResult CityDetail(string friendlyName, string detailfriendlyName = "")
        {
            var data = Repository.Get(w => w.Lang == UserLang && w.FriendlyName == friendlyName);
            if (data == null)
            {
                return RedirectToAction("Index", "City").Error("Ülke Bulunamadı !");
            }
            ViewData["Details"] = _detailRepo.GetList(w => w.Lang == UserLang && w.EntityRelationType == EntityRelationType.City && w.RelatedId == data.MasterId).ToList();
            ViewData["SelectedDetail"] = detailfriendlyName;
            return View(data);
        }
    }
}
