using Microsoft.AspNetCore.Mvc;
using BaseEntities.EnumTypes;
using Core.Attributes;
using BarameBroad.WebUI.Base;
using BaseEntities.Database;
using Core.DataAccess.Abstract;
using DevExtreme.AspNet.Mvc;
using Extensions.UIExtensions.ControllerExtensions;
using CrossCuttingConcerns.BaseControllers;
using Microsoft.EntityFrameworkCore;
using BaseEntities.Response;
using BaseEntities.Concrete;
using Extensions.FriendlyNameExtensions;
using MemCaching.Abstract;

namespace BarameBroad.WebUI.Areas.Blog.Controllers
{
    [Area("Blog")]
    [InRole(UserRole.Admin)]
    public class BlogCategoryController : BaseController<MasterBlogCategory, BaseEntities.Database.BlogCategory>
    {
        IQueryableRepositoryBase<BaseEntities.Database.BlogCategory> _blogCategories;
        public BlogCategoryController(IEntityRepositoryBase<MasterBlogCategory, BaseEntities.Database.BlogCategory> repo, ICacheService cacheService, IQueryableRepositoryBase<BaseEntities.Database.BlogCategory> blogCategories) : base(repo, cacheService)
        {
            _blogCategories = blogCategories;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GridSource(DataSourceLoadOptions loadOptions)
        {
            return Content(_blogCategories.DxQueryableGridList<BaseEntities.Database.BlogCategory>(loadOptions, _blogCategories.Table.Where(w => w.Lang == "tr-TR").AsQueryable(), _blogCategories.Table).ToString(), "application/json");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(Guid id)
        {
            return View(Repository.Get(w => w.Id == id, new System.Linq.Expressions.Expression<Func<MasterBlogCategory, object>>[] { w => w.Childrens }));
        }

        [HttpPost]
        public IActionResult Create(MasterBlogCategory model)
        {
            foreach (var item in model.Childrens)
            {
                item.FriendlyName = item.Name.GetFriendlyTitle();
                if (_blogCategories.Table.Any(w => w.FriendlyName == item.FriendlyName && w.Lang == item.Lang))
                {
                    return View(model).Error($"Aynı isim ile kayıtlı veri mevcut : {item.Name}");
                }
            }
            if (Repository.CreateWithSubEntities(model))
                return RedirectToAction("Index", "BlogCategory", new { area = "Blog" }).Success(Constants.Success);

            return View(model).Error(Constants.Error);
        }

        [HttpPost]
        public IActionResult Update(MasterBlogCategory model)
        {
            foreach (var item in model.Childrens)
            {
                item.FriendlyName = item.Name.GetFriendlyTitle();
                if (_blogCategories.Table.Any(w => w.FriendlyName == item.FriendlyName && w.Lang == item.Lang && w.Id != item.Id))
                {
                    return View(model).Error($"Aynı isim ile kayıtlı veri mevcut : {item.Name}");
                }
            }

            if (Repository.UpdateWithSubEntities(model))
                return RedirectToAction("Index", "BlogCategory", new { area = "Blog" }).Success(Constants.Success);

            return View(model).Error(Constants.Error);
        }

        public IActionResult Delete(Guid id)
        {
            if (Repository.SetState(Repository.Get(w => w.Id == id), EntityState.Deleted))
                return Json(new DataResponse { Success = true, Message = Constants.Success });

            return Json(new DataResponse { Success = false, Message = Constants.Error });
        }

        public IActionResult CategorySelectPartial(Guid? selected = null)
        {
            var data = _blogCategories.Table.Where(w => w.Lang == "tr-TR").Select(w => new SelectBoxModel
            {
                DisplayValue = w.Name,
                Id = w.Id,
            }).ToList();
            ViewData["SelectedBlogCategory"] = selected;

            return PartialView("Partials/_CategorySelectPartial", data);
        }

    }
}
