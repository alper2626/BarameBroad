using Microsoft.AspNetCore.Mvc;
using BaseEntities.EnumTypes;
using Core.Attributes;
using Core.DataAccess.Abstract;
using BarameBroad.WebUI.Base;
using BaseEntities.Database;
using DevExtreme.AspNet.Mvc;
using Extensions.FriendlyNameExtensions;
using Extensions.UIExtensions.ControllerExtensions;
using CrossCuttingConcerns.BaseControllers;
using Microsoft.EntityFrameworkCore;
using BaseEntities.Response;
using Extensions.UiExtensions;
using MemCaching.Abstract;

namespace BarameBroad.WebUI.Areas.Blog.Controllers
{
    [Area("Blog")]
    [InRole(UserRole.Admin)]
    public class BlogController : BaseController<MasterBlog, BlogEntity>
    {
        IQueryableRepositoryBase<BlogEntity> _blog;
        public BlogController(IEntityRepositoryBase<MasterBlog, BlogEntity> repo, ICacheService cacheService, IQueryableRepositoryBase<BlogEntity> blog) : base(repo, cacheService)
        {
            _blog = blog;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GridSource(DataSourceLoadOptions loadOptions)
        {
            return Content(_blog.DxQueryableGridList<BlogEntity>(loadOptions, _blog.Table.Where(w => w.Lang == "tr-TR").Include(w=>w.Master).ThenInclude(w=>w.BlogCategory).AsQueryable(), _blog.Table).ToString(), "application/json");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(Guid id)
        {
            return View(Repository.Get(w => w.Id == id, new System.Linq.Expressions.Expression<Func<MasterBlog, object>>[] { w => w.Childrens, w => w.BlogCategory }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(MasterBlog model)
        {
            foreach (var item in model.Childrens)
            {
                item.FriendlyName = item.Name.GetFriendlyTitle();
                if (_blog.Table.Any(w => w.FriendlyName == item.FriendlyName && w.Lang == item.Lang))
                {
                    return View(model).Error($"Aynı isim ile kayıtlı veri mevcut : {item.Name}");
                }

                item.CreateTime = DateTime.Now;
                item.UpdateTime = DateTime.Now;
            }
            if (Repository.CreateWithSubEntities(model))
            {
                CacheService.Delete("Blogs");
                await StaticFileOperations.UploadFiles("Blog", model.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Success = true, Message = Constants.Success });
                }
                return RedirectToAction("Index", "Blog", new { area = "Blog" }).Success(Constants.Success);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new DataResponse { Success = true, Message = Constants.Error });
            }

            return View(model).Error(Constants.Error);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MasterBlog model)
        {
            foreach (var item in model.Childrens)
            {
                item.FriendlyName = item.Name.GetFriendlyTitle();
                if (_blog.Table.Any(w => w.FriendlyName == item.FriendlyName && w.Lang == item.Lang && w.Id != item.Id))
                {
                    return View(model).Error($"Aynı isim ile kayıtlı veri mevcut : {item.Name}");
                }

                item.UpdateTime = DateTime.Now;
            }

            if (Repository.UpdateWithSubEntities(model))
            {
                CacheService.Delete("Blogs");
                await StaticFileOperations.UploadFiles("Blog", model.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Success = true, Message = Constants.Success });
                }

                return RedirectToAction("Index", "Blog", new { area = "Blog" }).Success(Constants.Success);
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
                CacheService.Delete("Blogs");
                StaticFileOperations.DirectoryDelete("Blog",id);
                return Json(new DataResponse { Success = true, Message = Constants.Success });
            }
                

            return Json(new DataResponse { Success = false, Message = Constants.Error });
        }
    }
}
