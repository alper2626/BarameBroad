using System;
using BarameBroad.WebUI.Base;
using BaseEntities.Concrete;
using BaseEntities.Database;
using BaseEntities.EnumTypes;
using Core.Attributes;
using Core.DataAccess.Abstract;
using CrossCuttingConcerns.BaseControllers;
using Extensions.UIExtensions.ControllerExtensions;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarameBroad.WebUI.Areas.Seo.Controllers
{
    [Area("Seo")]
    [InRole(UserRole.Admin)]
    public class SeoController : BaseController<MasterSeoEntity, SeoEntity>
    {
        public SeoController(IEntityRepositoryBase<MasterSeoEntity, SeoEntity> repo, ICacheService cacheService) : base(repo, cacheService)
        {
        }

        public IActionResult UpdateSeo(Guid relatedId, string key, string show)
        {
            var master = new MasterSeoEntity
            {
                Id = Guid.NewGuid(),
                TableName = key,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                RelatedId = relatedId,
                Childrens = new List<SeoEntity>
                {
                    new SeoEntity { Id = Guid.NewGuid(), Title = "Sayfa Başlığı",Description="Google Açıklaması",Keywords="Anahtar Kelimeler",Lang="tr-TR" },
                    new SeoEntity { Id = Guid.NewGuid(), Title = "Sayfa Başlığı",Description="Google Açıklaması",Keywords="Anahtar Kelimeler",Lang="en-US" },
                }
            };
            var response = Repository.CreateAndGet(w => w.RelatedId == relatedId, master, new System.Linq.Expressions.Expression<Func<MasterSeoEntity, object>>[] { w => w.Childrens });
            if (response == null)
                return View(master).Error(Constants.Error);

            return View(response);
        }

        [HttpPost]
        public IActionResult UpdateSeo(MasterSeoEntity model)
        {
            if (Repository.UpdateWithSubEntities(model))
                return RedirectToAction("Index", "Home", new { area = "Admin" }).Success(Constants.Success);

            return View().Error(Constants.Error);


        }
    }
}
