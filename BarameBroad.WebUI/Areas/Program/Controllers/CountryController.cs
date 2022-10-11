using BarameBroad.WebUI.Base;
using BaseEntities.Concrete;
using BaseEntities.Database;
using BaseEntities.EnumTypes;
using BaseEntities.Response;
using Core.Attributes;
using Core.DataAccess.Abstract;
using CrossCuttingConcerns.BaseControllers;
using DevExtreme.AspNet.Mvc;
using Extensions.FriendlyNameExtensions;
using Extensions.UiExtensions;
using Extensions.UIExtensions.ControllerExtensions;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarameBroad.WebUI.Areas.Program.Controllers
{
    [Area("Program")]
    [InRole(UserRole.Admin)]
    public class CountryController : BaseController<MasterCountry, CountryEntity>
    {
        IQueryableRepositoryBase<CountryEntity> _country;
        public CountryController(IEntityRepositoryBase<MasterCountry, CountryEntity> repo, ICacheService cacheService, IQueryableRepositoryBase<CountryEntity> country) : base(repo, cacheService)
        {

            _country = country;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GridSource(DataSourceLoadOptions loadOptions)
        {
            return Content(_country.DxQueryableGridList<CountryEntity>(loadOptions, _country.Table.Where(w => w.Lang == "tr-TR").AsQueryable(), _country.Table).ToString(), "application/json");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(Guid id)
        {
            return View(Repository.Get(w => w.Id == id, new System.Linq.Expressions.Expression<Func<MasterCountry, object>>[] { w => w.Childrens }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(MasterCountry model)
        {
            foreach (var item in model.Childrens)
            {
                item.FriendlyName = item.Name.GetFriendlyTitle();
                if (_country.Table.Any(w => w.FriendlyName == item.FriendlyName && w.Lang == item.Lang))
                {
                    return View(model).Error($"Aynı isim ile kayıtlı veri mevcut : {item.Name}");
                }
                model.Name = item.Name;
            }
            if (Repository.CreateWithSubEntities(model))
            {
                CacheService.Delete("Countries");
                await StaticFileOperations.UploadFiles("Country", model.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse {Data=model.Id, Success = true, Message = Constants.Success });
                }
                return RedirectToAction("Update", "Country", new { area = "Program", id = model.Id }).Success(Constants.Success);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new DataResponse { Success = false, Message = Constants.Error });
            }

            return View(model).Error(Constants.Error);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MasterCountry model)
        {
            foreach (var item in model.Childrens)
            {
                item.FriendlyName = item.Name.GetFriendlyTitle();
                if (_country.Table.Any(w => w.FriendlyName == item.FriendlyName && w.Lang == item.Lang && w.Id != item.Id))
                {
                    return View(model).Error($"Aynı isim ile kayıtlı veri mevcut : {item.Name}");
                }
                model.Name = item.Name;
            }

            if (Repository.UpdateWithSubEntities(model))
            {
                CacheService.Delete("Countries");
                await StaticFileOperations.UploadFiles("Country", model.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Success = true, Message = Constants.Success });
                }

                return RedirectToAction("Index", "Country", new { area = "Program" }).Success(Constants.Success);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new DataResponse { Success = true, Message = Constants.Error });
            }

            return View(model).Error(Constants.Error);
        }

        public IActionResult Delete(Guid id)
        {
            if (Repository.SetState(Repository.Get(w => w.Id == id), EntityState.Deleted))
            {
                CacheService.Delete("Countries");
                StaticFileOperations.DirectoryDelete("Country", id);
                return Json(new DataResponse { Success = true, Message = Constants.Success });
            }
            
            return Json(new DataResponse { Success = false, Message = Constants.Error });
        }

        public IActionResult CountrySelectPartial(Guid? selected = null)
        {
            var data = _country.Table.Where(w => w.Lang == "tr-TR").Select(w => new SelectBoxModel
            {
                DisplayValue = w.Name,
                Id = w.MasterId,
            }).ToList();
            ViewData["SelectedCountry"] = selected;

            return PartialView("Partials/_CountrySelectPartial", data);
        }

        public IActionResult CountryMultipleSelectPartial(List<Guid> selecteds)
        {
            var data = _country.Table.Where(w => w.Lang == "tr-TR").Select(w => new SelectBoxModel
            {
                DisplayValue = w.Name,
                Id = w.MasterId,
            }).ToList();
            ViewData["SelectedCountry"] = selecteds;

            return PartialView("Partials/_CountryMultipleSelectPartial", data);
        }

    }
}