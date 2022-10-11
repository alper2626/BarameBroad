using BaseEntities.Database;
using BaseEntities.EnumTypes;
using BaseEntities.Response;
using Core.Attributes;
using Core.DataAccess.Abstract;
using CrossCuttingConcerns.BaseControllers;
using DevExtreme.AspNet.Mvc;
using Extensions.UIExtensions.ControllerExtensions;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarameBroad.WebUI.Areas.SiteConfig.Controllers
{
    [Area("SiteConfig")]
    [InRole(UserRole.Admin)]
    public class SocialNetworkController : Controller
    {
        private IEntityRepositoryBase<SocialNetworkEntity> _social;
        IQueryableRepositoryBase<SocialNetworkEntity> _socialQueryable;
        ICacheService _cacheService;
        public SocialNetworkController(IEntityRepositoryBase<SocialNetworkEntity> social, ICacheService cacheService, IQueryableRepositoryBase<SocialNetworkEntity> socialQueryable)
        {
            _social = social;
            _socialQueryable = socialQueryable;
            _cacheService = cacheService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GridSource(DataSourceLoadOptions loadOptions)
        {
            return Content(_socialQueryable.DxGridList<SocialNetworkEntity>(loadOptions, _socialQueryable.Table).ToString(), "application/json");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(Guid id)
        {
            return View(_social.Get(w => w.Id == id));
        }

        [HttpPost]
        public IActionResult Create(SocialNetworkEntity model)
        {
            var response = _social.SetStateEntity(model, EntityState.Added);
            if (response != null)
            {
                _cacheService.Delete("SocialNetworks");
                return RedirectToAction("Index", "SocialNetwork", new { area = "SiteConfig" }).Success(Constants.Success);
            }
                

            return View(model).Error(Constants.Error);


        }

        [HttpPost]
        public IActionResult Update(SocialNetworkEntity model)
        {
            var response = _social.SetStateEntity(model, EntityState.Modified);
            if (response != null)
            {
                _cacheService.Delete("SocialNetworks");
                return RedirectToAction("Index", "SocialNetwork", new { area = "SiteConfig" }).Success(Constants.Success);
            }
                

            return View(model).Error(Constants.Error);

        }

        public IActionResult Delete(Guid id)
        {
            if (_social.SetState(_social.Get(w => w.Id == id), EntityState.Deleted))
            {
                _cacheService.Delete("SocialNetworks");

                return Json(new DataResponse { Success = true, Message = Constants.Success });
            }
                

            return Json(new DataResponse { Success = false, Message = Constants.Error });
        }
    }
}
