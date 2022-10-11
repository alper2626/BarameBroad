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
using System.Transactions;

namespace BarameBroad.WebUI.Areas.Program.Controllers
{
    [Area("Program")]
    [InRole(UserRole.Admin)]
    public class ProgramController : BaseController<MasterProgram, ProgramEntity>
    {
        IQueryableRepositoryBase<ProgramEntity> _program;
        IEntityRepositoryBase<ProgramRelation> _programRelation;
        public ProgramController(IEntityRepositoryBase<MasterProgram, ProgramEntity> repo, ICacheService cacheService, IQueryableRepositoryBase<ProgramEntity> program, IEntityRepositoryBase<ProgramRelation> programRelation) : base(repo, cacheService)
        {

            _program = program;
            _programRelation = programRelation;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GridSource(DataSourceLoadOptions loadOptions)
        {
            return Content(_program.DxQueryableGridList<ProgramEntity>(loadOptions, _program.Table.Where(w => w.Lang == "tr-TR").Include(w => w.Master).AsQueryable(), _program.Table).ToString(), "application/json");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(Guid id)
        {
            var dto = new ProgramDto
            {
                MasterProgram = Repository.Get(w => w.Id == id, new System.Linq.Expressions.Expression<Func<MasterProgram, object>>[] { w => w.Childrens, w => w.MasterProgramEntity, w => w.ProgramRelations }),
            };
            dto.Cities = dto.MasterProgram.ProgramRelations.Where(w => w.EntityRelationType == EntityRelationType.City).Select(q => q.RelatedId).ToList();
            dto.Countries = dto.MasterProgram.ProgramRelations.Where(w => w.EntityRelationType == EntityRelationType.Country).Select(q => q.RelatedId).ToList();
            dto.Schools = dto.MasterProgram.ProgramRelations.Where(w => w.EntityRelationType == EntityRelationType.School).Select(q => q.RelatedId).ToList();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProgramDto model)
        {
            foreach (var item in model.MasterProgram.Childrens)
            {
                item.FriendlyName = item.Name.GetFriendlyTitle();
                if (_program.Table.Any(w => w.FriendlyName == item.FriendlyName && w.Lang == item.Lang))
                {
                    return View(model).Error($"Aynı isim ile kayıtlı veri mevcut : {item.Name}");
                }
                model.MasterProgram.Name = item.Name;
            }
            model.MasterProgram.ProgramRelations = new List<ProgramRelation>();
            AddItemToProgramRelation(model.MasterProgram.ProgramRelations, model.Countries, EntityRelationType.Country, model.MasterProgram.Id);
            AddItemToProgramRelation(model.MasterProgram.ProgramRelations, model.Cities, EntityRelationType.City, model.MasterProgram.Id);
            AddItemToProgramRelation(model.MasterProgram.ProgramRelations, model.Schools, EntityRelationType.School, model.MasterProgram.Id);

            if (Repository.CreateWithSubEntities(model.MasterProgram))
            {
                if (model.MasterProgram.MasterProgramId == null || model.MasterProgram.MasterProgramId == Guid.Empty)
                {
                    CacheService.Delete("BaseServices");
                }

                await StaticFileOperations.UploadFiles("Program", model.MasterProgram.Id, Request.Form.Files);
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Data = model.MasterProgram.Id, Success = true, Message = Constants.Success });
                }
                return RedirectToAction("Update", "Program", new { area = "Program", id = model.MasterProgram.Id }).Success(Constants.Success);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new DataResponse { Success = false, Message = Constants.Error });
            }

            return View(model).Error(Constants.Error);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProgramDto model)
        {
            if (model.MasterProgram.Id == model.MasterProgram.MasterProgramId)
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Success = false, Message = Constants.Error });
                }

                return View(model).Error("Ana program ile girilen program aynı olamaz.");
            }
            foreach (var item in model.MasterProgram.Childrens)
            {
                item.FriendlyName = item.Name.GetFriendlyTitle();
                if (_program.Table.Any(w => w.FriendlyName == item.FriendlyName && w.Lang == item.Lang && w.Id != item.Id))
                {
                    return View(model).Error($"Aynı isim ile kayıtlı veri mevcut : {item.Name}");
                }
                model.MasterProgram.Name = item.Name;
            }

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (Repository.UpdateWithSubEntities(model.MasterProgram))
                    {
                        CacheService.Delete("BaseServices");

                        var programs = _programRelation.GetList(w => w.ProgramId == model.MasterProgram.Id);

                        _ = _programRelation.SetStateEntity(programs.ToList(), EntityState.Deleted);


                        var lst = new List<ProgramRelation>();
                        AddItemToProgramRelation(lst, model.Countries, EntityRelationType.Country, model.MasterProgram.Id);
                        AddItemToProgramRelation(lst, model.Cities, EntityRelationType.City, model.MasterProgram.Id);
                        AddItemToProgramRelation(lst, model.Schools, EntityRelationType.School, model.MasterProgram.Id);

                        _ = _programRelation.SetStateEntity(lst, EntityState.Added);



                        await StaticFileOperations.UploadFiles("Program", model.MasterProgram.Id, Request.Form.Files);

                        scope.Complete();

                        if (Request.IsAjaxRequest())
                        {
                            return Json(new DataResponse { Success = true, Message = Constants.Success });
                        }

                        return RedirectToAction("Index", "Program", new { area = "Program" }).Success(Constants.Success);
                    }

                    scope.Dispose();
                    if (Request.IsAjaxRequest())
                    {
                        return Json(new DataResponse { Success = false, Message = Constants.Error });
                    }

                    return View(model).Error(Constants.Error);
                }
                catch (Exception)
                {
                    scope.Dispose();
                    if (Request.IsAjaxRequest())
                    {
                        return Json(new DataResponse { Success = false, Message = Constants.Error });
                    }

                    return View(model).Error(Constants.Error);
                }
            }


        }

        public IActionResult Delete(Guid id)
        {
            if (!Repository.SetState(Repository.Get(w => w.Id == id), EntityState.Deleted))
                return Json(new DataResponse { Success = false, Message = Constants.Error });

            StaticFileOperations.DirectoryDelete("Program", id);

            CacheService.Delete("BaseServices");

            return Json(new DataResponse { Success = true, Message = Constants.Success });
        }

        public IActionResult ProgramSelectPartial(Guid? selected = null)
        {
            var data = _program.Table.Where(w => w.Lang == "tr-TR").Select(w => new SelectBoxModel
            {
                DisplayValue = w.Name,
                Id = w.MasterId,
            }).ToList();
            ViewData["SelectedProgram"] = selected;

            return PartialView("Partials/_ProgramSelectPartial", data);
        }

        private void AddItemToProgramRelation(List<ProgramRelation> list, List<Guid> items, EntityRelationType type, Guid programId)
        {
            if (items == null || items.Count == 0)
                return;

            list.AddRange(
                items.Select(w => new ProgramRelation
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    EntityRelationType = type,
                    RelatedId = w,
                    ProgramId = programId
                })
            );
        }
    }
}
