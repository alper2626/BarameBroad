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
    public class SchoolController : BaseController<MasterSchool, BaseEntities.Database.School>
    {
        IQueryableRepositoryBase<BaseEntities.Database.School> _school;
        public SchoolController(IEntityRepositoryBase<MasterSchool, BaseEntities.Database.School> repo, ICacheService cacheService, IQueryableRepositoryBase<BaseEntities.Database.School> school) : base(repo, cacheService)
        {

            _school = school;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GridSource(DataSourceLoadOptions loadOptions)
        {
            return Content(_school.DxQueryableGridList<BaseEntities.Database.School>(loadOptions, _school.Table.Where(w => w.Lang == "tr-TR").Include(w => w.Master).ThenInclude(w => w.City).AsQueryable(), _school.Table).ToString(), "application/json");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(Guid id)
        {
            return View(Repository.Get(w => w.Id == id, new System.Linq.Expressions.Expression<Func<MasterSchool, object>>[] { w => w.Childrens, w => w.City }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(MasterSchool model)
        {
            foreach (var item in model.Childrens)
            {
                item.FriendlyName = item.Name.GetFriendlyTitle();
                if (_school.Table.Any(w => w.FriendlyName == item.FriendlyName && w.Lang == item.Lang))
                {
                    return View(model).Error($"Aynı isim ile kayıtlı veri mevcut : {item.Name}");
                }

                model.Name = item.Name;
            }
            if (Repository.CreateWithSubEntities(model))
            {
                await StaticFileOperations.UploadFiles("School", model.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Data = model.Id, Success = true, Message = Constants.Success });
                }
                return RedirectToAction("Update", "School", new { area = "Program", id = model.Id }).Success(Constants.Success);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new DataResponse { Success = false, Message = Constants.Error });
            }

            return View(model).Error(Constants.Error);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MasterSchool model)
        {
            foreach (var item in model.Childrens)
            {
                item.FriendlyName = item.Name.GetFriendlyTitle();
                if (_school.Table.Any(w => w.FriendlyName == item.FriendlyName && w.Lang == item.Lang && w.Id != item.Id))
                {
                    return View(model).Error($"Aynı isim ile kayıtlı veri mevcut : {item.Name}");
                }

                model.Name = item.Name;
            }

            if (Repository.UpdateWithSubEntities(model))
            {
                await StaticFileOperations.UploadFiles("School", model.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Success = true, Message = Constants.Success });
                }

                return RedirectToAction("Index", "School", new { area = "Program" }).Success(Constants.Success);
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

        public IActionResult SchoolSelectPartial(Guid? selected = null)
        {
            var data = _school.Table.Where(w => w.Lang == "tr-TR").Select(w => new SelectBoxModel
            {
                DisplayValue = w.Name,
                Id = w.MasterId,
            }).ToList();
            ViewData["SelectedSchool"] = selected;

            return PartialView("Partials/_SchoolSelectPartial", data);
        }

        public IActionResult SchoolMultipleSelectPartial(List<Guid> selecteds)
        {
            var data = _school.Table.Where(w => w.Lang == "tr-TR").Select(w => new SelectBoxModel
            {
                DisplayValue = w.Name,
                Id = w.MasterId,
            }).ToList();
            ViewData["SelectedSchool"] = selecteds;

            return PartialView("Partials/_SchoolMultipleSelectPartial", data);
        }

    }
}