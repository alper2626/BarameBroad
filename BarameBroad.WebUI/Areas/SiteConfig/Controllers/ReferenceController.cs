using BaseEntities.Database;
using BaseEntities.EnumTypes;
using BaseEntities.Response;
using Core.Attributes;
using Core.DataAccess.Abstract;
using CrossCuttingConcerns.BaseControllers;
using DevExtreme.AspNet.Mvc;
using Extensions.UiExtensions;
using Extensions.UIExtensions.ControllerExtensions;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarameBroad.WebUI.Areas.SiteConfig.Controllers
{
    [Area("SiteConfig")]
    [InRole(UserRole.Admin)]
    public class ReferenceController : Controller
    {
        private IEntityRepositoryBase<Reference> _reference;
        IQueryableRepositoryBase<Reference> _referenceQueryable;
        ICacheService _cacheService;
        public ReferenceController(IEntityRepositoryBase<Reference> reference, ICacheService cacheService, IQueryableRepositoryBase<Reference> referenceQueryable)
        {
            _reference = reference;
            _referenceQueryable = referenceQueryable;
            _cacheService = cacheService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GridSource(DataSourceLoadOptions loadOptions)
        {
            return Content(_referenceQueryable.DxGridList<Reference>(loadOptions, _referenceQueryable.Table).ToString(), "application/json");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(Guid id)
        {
            return View(_reference.Get(w => w.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reference model)
        {
            var response = _reference.SetStateEntity(model, EntityState.Added);
            if (response != null)
            {
                _cacheService.Delete("References");
                await StaticFileOperations.UploadFiles("Reference", model.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Success = true, Message = Constants.Success });
                }
                return RedirectToAction("Index", "Reference", new { area = "SiteConfig" }).Success(Constants.Success);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new DataResponse { Success = true, Message = Constants.Error });
            }

            return View(model).Error(Constants.Error);


        }

        [HttpPost]
        public async Task<IActionResult> Update(Reference model)
        {
            var response = _reference.SetStateEntity(model, EntityState.Modified);
            if (response != null)
            {
                _cacheService.Delete("References");
                await StaticFileOperations.UploadFiles("Reference", model.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Success = true, Message = Constants.Success });
                }

                return RedirectToAction("Index", "Reference", new { area = "SiteConfig" }).Success(Constants.Success);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new DataResponse { Success = true, Message = Constants.Error });
            }


            return View(model).Error(Constants.Error);
        }

        public IActionResult Delete(Guid id)
        {
            if (_reference.SetState(_reference.Get(w => w.Id == id), EntityState.Deleted))
            {
                _cacheService.Delete("References");
                StaticFileOperations.DirectoryDelete("Reference", id);
                return Json(new DataResponse { Success = true, Message = Constants.Success });
            }
                

            return Json(new DataResponse { Success = false, Message = Constants.Error });
        }
    }
}
