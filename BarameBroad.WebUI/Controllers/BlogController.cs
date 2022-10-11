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
    [Route("Blog")]
    public class BlogController : UserController<BlogEntity>
    {
        IQueryableRepositoryBase<BlogEntity> _blogQueryable;
        IEntityRepositoryBase<BaseEntities.Database.BlogCategory> _cRepo;
        public BlogController(IEntityRepositoryBase<BlogEntity> repo, ICacheService cacheService, IHttpContextAccessor httpContextAccessor, IQueryableRepositoryBase<BlogEntity> blogQueryable, IEntityRepositoryBase<BaseEntities.Database.BlogCategory> cRepo) : base(repo, cacheService, httpContextAccessor)
        {
            _blogQueryable = blogQueryable;
            _cRepo = cRepo;
        }

        [Route("anasayfa")]
        public IActionResult HomeBlogPartial()
        {
            var res = CacheService.Get<List<BlogEntity>>("Blogs");

            if (res == null)
            {
                res = Repository.GetListSkipTake(0,3).ToList();
                CacheService.AddOrUpdate("Blogs", res);
            }
            return PartialView("Partials/_HomeBlogPartial", res);
        }

        [Route("liste/{BlogCategoryFriendlyName}")]
        public IActionResult BlogPartial(BlogFilterModel filter)
        {
            var query = _blogQueryable.Table.Where(w => w.Lang == UserLang);
            if (filter.CId != null)
            {
                query = query.Where(w => w.Master.BlogCategoryId == filter.CId);
            }
            if (!string.IsNullOrEmpty(filter.BlogName))
            {
                query = query.Where(w => w.Name.ToLower().Contains(filter.BlogName.ToLower()));
            }
            if (filter.Page > 1)
            {
                query = query.Skip(filter.Page - 1 * filter.Take);
            }
            query = query.Take(filter.Take);
            return PartialView("Partials/_BlogPartial", query.OrderByDescending(w => w.Master.CreateTime).ToList());
        }

        [Route("detay/{friendlyName}")]
        public IActionResult BlogDetail(string friendlyName)
        {
            ViewData["Categories"] = _cRepo.GetList(w => w.Lang == UserLang).ToList();

            var ent = Repository.Get(w => w.FriendlyName == friendlyName && w.Lang == UserLang);
            if (ent == null)
                return RedirectToAction("Index", "Home").Error(Constants.Error);

            return View(ent);
        }
    }
}
