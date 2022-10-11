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
    [Route("ulkeler")]
    public class CountryController : UserController<CountryEntity>
    {
        IQueryableRepositoryBase<CountryEntity> _qRepo;
        IEntityRepositoryBase<ContentDetail> _detailRepo;
        public CountryController(IEntityRepositoryBase<CountryEntity> repo, ICacheService cacheService, IHttpContextAccessor httpContextAccessor, IQueryableRepositoryBase<CountryEntity> qRepo, IEntityRepositoryBase<ContentDetail> detailRepo) : base(repo, cacheService, httpContextAccessor)
        {
            _qRepo = qRepo;
            _detailRepo = detailRepo;
        }

        [Route("")]
        public IActionResult Index(FilterModel filter)
        {
            ViewData["Filters"] = filter;

            var query = _qRepo.Table.Where(w => w.Lang == UserLang);

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(w => w.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            var skip = (filter.Page - 1) * filter.Take;
            if (skip < 0)
            {
                skip = 0;
            }

            return View(query.Skip(skip).Take(filter.Take).OrderByDescending(w => w.CreateTime).ToList());
        }

        [Route("anasayfa")]
        public IActionResult HomePageCountries()
        {
            var data = Repository.GetList(w => w.Lang == UserLang).OrderBy(w => w.Name).ToList();
            return PartialView("Partials/_HomePagePartial", data);
        }

        [Route("ulke-detay/{friendlyName}/{detailfriendlyName?}")]
        public IActionResult CountryDetail(string friendlyName, string detailfriendlyName = "")
        {
            var data = Repository.Get(w => w.Lang == UserLang && w.FriendlyName == friendlyName);
            if (data == null)
            {
                return RedirectToAction("Index", "Country").Error("Ülke Bulunamadı !");
            }
            ViewData["Details"] = _detailRepo.GetList(w => w.Lang == UserLang && w.EntityRelationType == EntityRelationType.Country && w.RelatedId == data.MasterId).ToList();
            ViewData["SelectedDetail"] = detailfriendlyName;
            return View(data);
        }
    }
}
