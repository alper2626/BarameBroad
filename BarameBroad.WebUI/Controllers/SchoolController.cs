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
    [Route("okullar")]
    public class SchoolController : UserController<BaseEntities.Database.School>
    {
        IQueryableRepositoryBase<BaseEntities.Database.School> _qRepo;
        IEntityRepositoryBase<ContentDetail> _detailRepo;
        public SchoolController(IEntityRepositoryBase<BaseEntities.Database.School> repo, ICacheService cacheService, IHttpContextAccessor httpContextAccessor, IQueryableRepositoryBase<BaseEntities.Database.School> qRepo, IEntityRepositoryBase<ContentDetail> detailRepo) : base(repo, cacheService, httpContextAccessor)
        {
            _qRepo = qRepo;
            _detailRepo = detailRepo;
        }

        [Route("okul-detay/{friendlyName}/{detailfriendlyName?}")]
        public IActionResult SchoolDetail(string friendlyName, string detailfriendlyName = "")
        {
            var data = Repository.Get(w => w.Lang == UserLang && w.FriendlyName == friendlyName);
            if (data == null)
            {
                return RedirectToAction("Index", "School").Error("Ülke Bulunamadı !");
            }
            ViewData["Details"] = _detailRepo.GetList(w => w.Lang == UserLang && w.EntityRelationType == EntityRelationType.School && w.RelatedId == data.MasterId).ToList();
            ViewData["SelectedDetail"] = detailfriendlyName;
            return View(data);
        }
    }
}
