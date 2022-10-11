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

namespace BarameBroad.WebUI.Areas.SiteConfig.Controllers
{
    [Area("SiteConfig")]
    [InRole(UserRole.Admin)]
    public class TeamController : BaseController<MasterTeam, TeamEntity>
    {
        IQueryableRepositoryBase<TeamEntity> _team;
        public TeamController(IEntityRepositoryBase<MasterTeam, TeamEntity> repo, ICacheService cacheService, IQueryableRepositoryBase<TeamEntity> team) : base(repo, cacheService)
        {
            _team = team;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GridSource(DataSourceLoadOptions loadOptions)
        {
            return Content(_team.DxQueryableGridList<TeamEntity>(loadOptions, _team.Table.Where(w => w.Lang == "tr-TR").Include(w=>w.Master).AsQueryable(), _team.Table).ToString(), "application/json");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(Guid id)
        {
            return View(Repository.Get(w => w.Id == id, new System.Linq.Expressions.Expression<Func<MasterTeam, object>>[] { w => w.Childrens }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(MasterTeam model)
        {
            if (Repository.CreateWithSubEntities(model))
            {
                await StaticFileOperations.UploadFiles("Team", model.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Success = true, Message = Constants.Success });
                }
                return RedirectToAction("Index", "Team", new { area = "SiteConfig" }).Success(Constants.Success);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new DataResponse { Success = true, Message = Constants.Error });
            }

            return View(model).Error(Constants.Error);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MasterTeam model)
        {
            if (Repository.UpdateWithSubEntities(model))
            {
                await StaticFileOperations.UploadFiles("Team", model.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Success = true, Message = Constants.Success });
                }

                return RedirectToAction("Index", "Team", new { area = "SiteConfig" }).Success(Constants.Success);
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
    }
}
