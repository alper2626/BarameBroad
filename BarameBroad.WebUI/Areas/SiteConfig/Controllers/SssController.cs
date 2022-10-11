using System;
using Microsoft.AspNetCore.Mvc;
using BaseEntities.EnumTypes;
using Core.Attributes;
using DevExtreme.AspNet.Mvc;
using BaseEntities.Database;
using Core.DataAccess.Abstract;
using BarameBroad.WebUI.Base;
using Extensions.UIExtensions.ControllerExtensions;
using CrossCuttingConcerns.BaseControllers;
using BaseEntities.Response;
using Microsoft.EntityFrameworkCore;
using MemCaching.Abstract;

namespace BarameBroad.WebUI.Areas.SiteConfig.Controllers
{
    [Area("SiteConfig")]
    [InRole(UserRole.Admin)]
    public class SssController : BaseController<MasterSss,Sss>
    {
        IQueryableRepositoryBase<Sss> _sss;
        public SssController(IEntityRepositoryBase<MasterSss, Sss> repo, ICacheService cacheService, IQueryableRepositoryBase<Sss> sss) : base(repo, cacheService)
        {
            _sss = sss;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GridSource(DataSourceLoadOptions loadOptions)
        {
            return Content(_sss.DxQueryableGridList<Sss>(loadOptions, _sss.Table.Where(w => w.Lang == "tr-TR").AsQueryable(), _sss.Table).ToString(), "application/json");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(Guid id)
        {
            return View(Repository.Get(w => w.Id == id, new System.Linq.Expressions.Expression<Func<MasterSss, object>>[] { w => w.Childrens }));
        }

        [HttpPost]
        public IActionResult Create(MasterSss model)
        {
            if (Repository.CreateWithSubEntities(model))
                return RedirectToAction("Index", "Sss", new { area = "SiteConfig" }).Success(Constants.Success);

            return View(model).Error(Constants.Error);
        }

        [HttpPost]
        public IActionResult Update(MasterSss model)
        {
            if (Repository.UpdateWithSubEntities(model))
                return RedirectToAction("Index", "Sss", new { area = "SiteConfig" }).Success(Constants.Success);

            return View(model).Error(Constants.Error);
        }

        public IActionResult Delete(Guid id)
        {
            if (Repository.SetState(Repository.Get(w => w.Id == id), EntityState.Deleted))
                return Json(new DataResponse { Success = true, Message = Constants.Success });

            return Json(new DataResponse { Success = false, Message = Constants.Error });
        }

    }
}
