using BarameBroad.WebUI.Base;
using BarameBroad.WebUI.Models.FilterModel;
using BaseEntities.Database;
using Core.DataAccess.Abstract;
using CrossCuttingConcerns.BaseControllers;
using Extensions.UIExtensions.ControllerExtensions;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BarameBroad.WebUI.Controllers
{
    [Route("Bloglar")]
    public class BlogCategoryController : UserController<BaseEntities.Database.BlogCategory>
    {
        IQueryableRepositoryBase<BlogEntity> _blogRepo;
        public BlogCategoryController(IEntityRepositoryBase<BaseEntities.Database.BlogCategory> repo, ICacheService cacheService, IHttpContextAccessor httpContextAccessor, IQueryableRepositoryBase<BlogEntity> blogRepo) : base(repo, cacheService, httpContextAccessor)
        {
            _blogRepo = blogRepo;
        }

        [Route("/liste/{BlogCategoryFriendlyName?}")]
        public IActionResult Index(BlogFilterModel filter)
        {
            ViewData["Filters"] = filter;
            var blogQuery = _blogRepo.Table.Where(w => w.Lang == UserLang);

            ViewData["categoryName"] = filter.BlogCategoryFriendlyName;
            var list = Repository.GetList(w => w.Lang == UserLang).ToList();
            var selectedCategory = list.FirstOrDefault(w => w.FriendlyName == filter.BlogCategoryFriendlyName);
            if (selectedCategory != null)
            {
                blogQuery = blogQuery.Where(w => w.Master.BlogCategoryId == selectedCategory.Id);
                ViewData["selectedCategory"] = selectedCategory.Name;
            }
            if (!string.IsNullOrEmpty(filter.BlogName))
            {
                blogQuery = blogQuery.Where(w => w.Name.ToLower().Contains(filter.BlogName.ToLower()));
            }
            var skip = (filter.Page - 1) * filter.Take;
            if (skip < 0)
            {
                skip = 0;
            }
            ViewData["BlogList"] = blogQuery.Skip(skip).Take(filter.Take).OrderByDescending(w => w.CreateTime).ToList();

            return View(list);
        }
    }
}
