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
    public class CityController : BaseController<MasterCity, BaseEntities.Database.City>
    {
        IQueryableRepositoryBase<BaseEntities.Database.City> _city;
        public CityController(IEntityRepositoryBase<MasterCity, BaseEntities.Database.City> repo, ICacheService cacheService, IQueryableRepositoryBase<BaseEntities.Database.City> city) : base(repo, cacheService)
        {

            _city = city;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GridSource(DataSourceLoadOptions loadOptions)
        {
            return Content(_city.DxQueryableGridList<BaseEntities.Database.City>(loadOptions, _city.Table.Where(w => w.Lang == "tr-TR").Include(w => w.Master).ThenInclude(w => w.Country).AsQueryable(), _city.Table).ToString(), "application/json");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(Guid id)
        {
            return View(Repository.Get(w => w.Id == id, new System.Linq.Expressions.Expression<Func<MasterCity, object>>[] { w => w.Childrens, w => w.Country }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(MasterCity model)
        {
            foreach (var item in model.Childrens)
            {
                item.FriendlyName = item.Name.GetFriendlyTitle();
                if (_city.Table.Any(w => w.FriendlyName == item.FriendlyName && w.Lang == item.Lang))
                {
                    return View(model).Error($"Aynı isim ile kayıtlı veri mevcut : {item.Name}");
                }

                model.Name = item.Name;
            }
            if (Repository.CreateWithSubEntities(model))
            {
                await StaticFileOperations.UploadFiles("City", model.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse {Data=model.Id, Success = true, Message = Constants.Success });
                }
                return RedirectToAction("Update", "City", new { area = "Program", id = model.Id }).Success(Constants.Success);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new DataResponse { Success = false, Message = Constants.Error });
            }

            return View(model).Error(Constants.Error);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MasterCity model)
        {
            foreach (var item in model.Childrens)
            {
                item.FriendlyName = item.Name.GetFriendlyTitle();
                if (_city.Table.Any(w => w.FriendlyName == item.FriendlyName && w.Lang == item.Lang && w.Id != item.Id))
                {
                    return View(model).Error($"Aynı isim ile kayıtlı veri mevcut : {item.Name}");
                }

                model.Name = item.Name;
            }

            if (Repository.UpdateWithSubEntities(model))
            {
                await StaticFileOperations.UploadFiles("City", model.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Success = true, Message = Constants.Success });
                }

                return RedirectToAction("Index", "City", new { area = "Program" }).Success(Constants.Success);
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
                return Json(new DataResponse { Success = true, Message = Constants.Success });

            return Json(new DataResponse { Success = false, Message = Constants.Error });
        }

        public IActionResult CitySelectPartial(Guid? selected = null)
        {
            var data = _city.Table.Where(w => w.Lang == "tr-TR").Select(w => new SelectBoxModel
            {
                DisplayValue = w.Name,
                Id = w.MasterId,
            }).ToList();
            ViewData["SelectedCity"] = selected;

            return PartialView("Partials/_CitySelectPartial", data);
        }

        public IActionResult CityMultipleSelectPartial(List<Guid> selecteds)
        {
            var data = _city.Table.Where(w => w.Lang == "tr-TR").Select(w => new SelectBoxModel
            {
                DisplayValue = w.Name,
                Id = w.MasterId,
            }).ToList();
            ViewData["SelectedCity"] = selecteds;

            return PartialView("Partials/_CityMultipleSelectPartial", data);
        }

    }
}

