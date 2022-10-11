using BarameBroad.WebUI.Base;
using BarameBroad.WebUI.Models.FilterModel;
using BaseEntities.Database;
using BaseEntities.EnumTypes;
using Core.DataAccess.Abstract;
using Extensions.UIExtensions.ControllerExtensions;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace BarameBroad.WebUI.Controllers
{
    [Route("Program")]
    public class ProgramController : UserController<ProgramEntity>
    {
        IEntityRepositoryBase<MasterProgram> masterRepo;
        IQueryableRepositoryBase<ProgramEntity> _qRepo;
        IEntityRepositoryBase<ContentDetail> _detailRepo;
        public ProgramController(IEntityRepositoryBase<ProgramEntity> repo, ICacheService cacheService, IHttpContextAccessor httpContextAccessor, IEntityRepositoryBase<MasterProgram> masterRepo, IQueryableRepositoryBase<ProgramEntity> qRepo, IEntityRepositoryBase<ContentDetail> detailRepo) : base(repo, cacheService, httpContextAccessor)
        {
            this.masterRepo = masterRepo;
            _qRepo = qRepo;
            _detailRepo = detailRepo;
        }

        [Route("")]
        public IActionResult Index(FilterModel filter)
        {
            ViewData["Filters"] = filter;

            var query = _qRepo.Table.Where(w => w.Lang == UserLang);

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(w => w.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            var skip = (filter.Page - 1) * filter.Take;
            if (skip < 0)
            {
                skip = 0;
            }

            return View(query.Skip(skip).Take(filter.Take).OrderByDescending(w => w.CreateTime).ToList());
        }

        [Route("anasayfa")]
        public IActionResult HomePartial()
        {
            var res = CacheService.Get<List<MasterProgram>>("BaseServices");

            if (res == null)
            {
                res = masterRepo.GetList(w => w.MasterProgramId == null || w.MasterProgramId == Guid.Empty, new System.Linq.Expressions.Expression<Func<MasterProgram, object>>[] { w => w.Childrens }).ToList();
                CacheService.AddOrUpdate("BaseServices", res);
            }
            return PartialView("Partials/_HomePartial", res.Take(3).SelectMany(w => w.Childrens.Where(w => w.Lang == UserLang)).ToList());
        }

        [Route("program-detay/{friendlyName}/{detailfriendlyName?}")]
        public IActionResult ProgramDetail(string friendlyName, string detailfriendlyName = "")
        {
            var data = Repository.Get(w => w.Lang == UserLang && w.FriendlyName == friendlyName);
            if (data == null)
            {
                return RedirectToAction("Index", "Program").Error("Program Bulunamadı !");
            }

            ViewData["Details"] = _detailRepo.GetList(w => w.Lang == UserLang && w.EntityRelationType == EntityRelationType.Program && w.RelatedId == data.MasterId).ToList();
            ViewData["SelectedDetail"] = detailfriendlyName;

            return View(data);
        }

        [Route("nerede-dahil-olurum/{friendlyName}")]
        public async Task<IActionResult> ProgramAssignments(string friendlyName)
        {
            var data = Repository.Get(w => w.Lang == UserLang && w.FriendlyName == friendlyName);
            if (data == null)
            {
                return RedirectToAction("Index", "Program").Error("Program Bulunamadı !");
            }

            ViewData["Details"] = _detailRepo.GetList(w => w.Lang == UserLang && w.EntityRelationType == EntityRelationType.Program && w.RelatedId == data.MasterId).ToList();

            Hashtable table = new Hashtable();
            table.Add("Id", data.MasterId);
            table.Add("Lang", UserLang);
            table.Add("LinkBase", string.Format("{0}://{1}", Request.Scheme, Request.Host));
            ViewData["Relations"] = await _qRepo.ExecuteListSpMsSqlAsync("Sp_GetProgramDetail", table);

            return View(data);
        }

        [Route("altprogramlar/{parentId}")]
        public async Task<IActionResult> SubProgramsPartial(Guid parentId)
        {
            var masterItems = masterRepo.GetList(w => w.MasterProgramId == parentId, new System.Linq.Expressions.Expression<Func<MasterProgram, object>>[] { w => w.Childrens });
            var items = masterItems.SelectMany(w => w.Childrens.Where(w => w.Lang == UserLang)).ToList();
            return PartialView("Partials/_SubProgramsPartial",items);
        }

    }
}
