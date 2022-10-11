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
    public class ContentDetailController : BaseController<MasterContentDetail, ContentDetail>
    {
        IQueryableRepositoryBase<ContentDetail> _contentDetail;
        public ContentDetailController(IEntityRepositoryBase<MasterContentDetail, ContentDetail> repo, ICacheService cacheService, IQueryableRepositoryBase<ContentDetail> contentDetail) : base(repo, cacheService)
        {

            _contentDetail = contentDetail;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GridPartial(ContentDetailTempModel model)
        {
            return PartialView("Partials/_GridPartial", model);
        }

        public IActionResult GridSource(DataSourceLoadOptions loadOptions, ContentDetailGridParameter parameters)
        {
            return Content(_contentDetail.DxQueryableGridList<ContentDetail>(loadOptions, _contentDetail.Table.Where(w => w.Lang == "tr-TR" && w.RelatedId == parameters.Value && w.EntityRelationType == parameters.EntityRelationType).Include(w=>w.Master).AsQueryable(), _contentDetail.Table).ToString(), "application/json");
        }

        public IActionResult Create(ContentDetailTempModel model)
        {
            var viewModel = new MasterContentDetailWithTemp
            {
                ContentDetailTempModel = model
            };
            return View(viewModel);
        }

        public IActionResult Update(ContentDetailTempModel model,Guid id)
        {
            var viewModel = new MasterContentDetailWithTemp
            {
                ContentDetailTempModel = model,
                MasterContentDetail = Repository.Get(w => w.Id == id, new System.Linq.Expressions.Expression<Func<MasterContentDetail, object>>[] { w => w.Childrens })
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(MasterContentDetailWithTemp model)
        {
            foreach (var item in model.MasterContentDetail.Childrens)
            {
                item.EntityRelationType = model.MasterContentDetail.EntityRelationType;
                item.RelatedId = model.MasterContentDetail.RelatedId;

                item.FriendlyName = item.Title.GetFriendlyTitle();
                if (_contentDetail.Table.Any(w => w.FriendlyName == item.FriendlyName && w.Lang == item.Lang))
                {
                    return RedirectToAction("Create", "ContentDetail", new { area = "Program", EntityRelationType = model.ContentDetailTempModel.EntityRelationType, Value = model.ContentDetailTempModel.Value, DisplayText = model.ContentDetailTempModel.DisplayText }).Error($"Aynı isim ile kayıtlı veri mevcut : {item.Title}");
                }

                item.CreateTime = DateTime.Now;
                item.UpdateTime = DateTime.Now;
            }
            if (Repository.CreateWithSubEntities(model.MasterContentDetail))
            {
                await StaticFileOperations.UploadFiles("ContentDetail", model.MasterContentDetail.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Success = true, Message = Constants.Success });
                }
                return RedirectToAction("Create", "ContentDetail", new { area = "Program", EntityRelationType= model.ContentDetailTempModel.EntityRelationType, Value = model.ContentDetailTempModel.Value, DisplayText= model.ContentDetailTempModel.DisplayText }).Success(Constants.Success);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new DataResponse { Success = false, Message = Constants.Error });
            }

            return View(model).Error(Constants.Error);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePost(MasterContentDetailWithTemp model)
        {
            foreach (var item in model.MasterContentDetail.Childrens)
            {
                item.EntityRelationType = model.MasterContentDetail.EntityRelationType;
                item.RelatedId = model.MasterContentDetail.RelatedId;

                item.FriendlyName = item.Title.GetFriendlyTitle();
                if (_contentDetail.Table.Any(w => w.FriendlyName == item.FriendlyName && w.Lang == item.Lang && w.Id != item.Id))
                {
                    return RedirectToAction("Update", "ContentDetail", new { area = "Program", EntityRelationType = model.ContentDetailTempModel.EntityRelationType, Value = model.ContentDetailTempModel.Value, DisplayText = model.ContentDetailTempModel.DisplayText, id = model.MasterContentDetail.Id }).Error($"Aynı isim ile kayıtlı veri mevcut : {item.Title}");
                }
            }

            if (Repository.UpdateWithSubEntities(model.MasterContentDetail))
            {
                await StaticFileOperations.UploadFiles("ContentDetail", model.MasterContentDetail.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Success = true, Message = Constants.Success });
                }

                return RedirectToAction("Update", "ContentDetail", new { area = "Program", EntityRelationType = model.ContentDetailTempModel.EntityRelationType, Value = model.ContentDetailTempModel.Value, DisplayText = model.ContentDetailTempModel.DisplayText, id=model.MasterContentDetail.Id }).Success(Constants.Success);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new DataResponse { Success = false, Message = Constants.Error });
            }

            return RedirectToAction("Update", "ContentDetail", new { area = "Program", EntityRelationType = model.ContentDetailTempModel.EntityRelationType, Value = model.ContentDetailTempModel.Value, DisplayText = model.ContentDetailTempModel.DisplayText, id = model.MasterContentDetail.Id }).Error(Constants.Error);
        }

        public IActionResult Delete(Guid id)
        {
            if (Repository.SetState(Repository.Get(w => w.Id == id), EntityState.Deleted))
                return Json(new DataResponse { Success = true, Message = Constants.Success });

            return Json(new DataResponse { Success = false, Message = Constants.Error });
        }
    }
}
