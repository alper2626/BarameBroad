using Microsoft.AspNetCore.Mvc;
using System.Net;
using BaseEntities.EnumTypes;
using BaseEntities.Response;
using Core.Attributes;
using BarameBroad.WebUI.Base;
using BaseEntities.Database;
using BaseEntities.Concrete;
using Extensions.UIExtensions.ControllerExtensions;
using Core.DataAccess.Abstract;
using Extensions.UiExtensions;
using CrossCuttingConcerns.BaseControllers;
using MemCaching.Abstract;

namespace BarameBroad.WebUI.Areas.SiteConfig.Controllers
{
    [Area("SiteConfig")]
    [InRole(UserRole.Admin)]
    public class SiteConfigController : BaseController<MasterSiteConfig, BaseEntities.Database.SiteConfig>
    {
        public SiteConfigController(IEntityRepositoryBase<MasterSiteConfig, BaseEntities.Database.SiteConfig> repo, ICacheService cacheService) : base(repo,cacheService)
        {
        }

        public IActionResult Index()
        {
            return View(base.Repository.First(new System.Linq.Expressions.Expression<Func<MasterSiteConfig, object>>[] { w => w.Childrens }));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSiteConfig(MasterSiteConfig siteConfig)
        {
            if (Repository.UpdateWithSubEntities(siteConfig))
            {
                CacheService.AddOrUpdate("SiteConfig", Repository.First(new System.Linq.Expressions.Expression<Func<MasterSiteConfig, object>>[] { w => w.Childrens }));
                await StaticFileOperations.UploadFiles("SiteConfig", siteConfig.Id, Request.Form.Files);

                if (Request.IsAjaxRequest())
                {
                    return Json(new DataResponse { Success = true, Message = Constants.Success });
                }
                return RedirectToAction("Index", "SiteConfig", new { area = "SiteConfig" }).Success(Constants.Success);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new DataResponse { Success = true, Message = Constants.Error});
            }

            return RedirectToAction("Index", "SiteConfig", new { area = "SiteConfig" }).Error(Constants.Error);
        }


        public IActionResult UploadLogo()
        {
            return View();
        }

        public IActionResult UploadFooterLogo()
        {
            return View();
        }




        public IActionResult UploadFav()
        {
            return View();
        }

        public IActionResult UploadSlider()
        {
            return View();
        }

        public IActionResult PostSlider()
        {
            DataResponse response;
            var imageResponse = StaticFileOperations.UploadFiles(StaticFileOperations.StaticFileTypes.Slider.ToString(),
                HttpContext.Request.Form.Files).Result;
            if (imageResponse)
                response = new DataResponse
                {
                    Success = true,
                    Message = "Resim Düzenlendi",
                    StatusCode = HttpStatusCode.OK
                };
            else
                response = new DataResponse
                {
                    Success = false,
                    Message = "Resim Düzenlenirken Hata Oluştu",
                    StatusCode = HttpStatusCode.BadRequest
                };
            if (Request.IsAjaxRequest())
                return Json(response);
            return RedirectToAction("UploadSlider", "SiteConfig", new { area = "SiteConfig" });




        }


        public IActionResult PostLogo()
        {
            DataResponse response;
            var imageResponse = StaticFileOperations.SaveFileDeleteOther(HttpContext.Request.Form.Files, StaticFileOperations.StaticFileTypes.Logo.ToString()).Result;
            if (imageResponse)
                response = new DataResponse
                {
                    Success = true,
                    Message = "Resim Düzenlendi",
                    StatusCode = HttpStatusCode.OK
                };
            else
                response = new DataResponse
                {
                    Success = false,
                    Message = "Resim Düzenlenirken Hata Oluştu",
                    StatusCode = HttpStatusCode.BadRequest
                };
            if (Request.IsAjaxRequest())
                return Json(response);
            return RedirectToAction("UploadSlider", "SiteConfig", new { area = "SiteConfig" });
        }

        public IActionResult PostFooterLogo()
        {
            DataResponse response;
            var imageResponse = StaticFileOperations.SaveFileDeleteOther(HttpContext.Request.Form.Files, StaticFileOperations.StaticFileTypes.FooterLogo.ToString()).Result;
            if (imageResponse)
                response = new DataResponse
                {
                    Success = true,
                    Message = "Resim Düzenlendi",
                    StatusCode = HttpStatusCode.OK
                };
            else
                response = new DataResponse
                {
                    Success = false,
                    Message = "Resim Düzenlenirken Hata Oluştu",
                    StatusCode = HttpStatusCode.BadRequest
                };
            if (Request.IsAjaxRequest())
                return Json(response);
            return RedirectToAction("UploadSlider", "SiteConfig", new { area = "SiteConfig" });
        }

        public IActionResult PostFav()
        {
            DataResponse response;
            var imageResponse = StaticFileOperations.SaveFileDeleteOther(HttpContext.Request.Form.Files, StaticFileOperations.StaticFileTypes.Fav.ToString()).Result;
            if (imageResponse)
                response = new DataResponse
                {
                    Success = true,
                    Message = "Resim Düzenlendi",
                    StatusCode = HttpStatusCode.OK
                };
            else
                response = new DataResponse
                {
                    Success = false,
                    Message = "Resim Düzenlenirken Hata Oluştu",
                    StatusCode = HttpStatusCode.BadRequest
                };
            if (Request.IsAjaxRequest())
                return Json(response);
            return RedirectToAction("UploadSlider", "SiteConfig", new { area = "SiteConfig" });
        }

        //public IActionResult SiteMapCreator()
        //{
        //    return View();
        //}


        //public IActionResult CreateSiteMap(decimal priority,SiteMapFreq freq)
        //{
        //    var res = SiteMapBuilder.Build(_siteConfigService.CreateSiteMap(), priority, freq);
        //    return File(Encoding.UTF8.GetBytes(res.ToString()), "application/xml", "sitemap1.xml");
        //}

        //public IActionResult CreateSiteMapGroup()
        //{
        //    var res = $"{this.Request.Scheme}://{this.Request.Host}/";
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //    sb.AppendLine("<sitemapindex xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

        //    for (int i = 1; i <= 5; i++)
        //    {
        //        sb.AppendLine("<sitemap>");
        //        sb.AppendLine("<loc>");
        //        sb.AppendLine($"{res}sitemap{i}.xml");
        //        sb.AppendLine("</loc>");
        //        sb.AppendLine("</sitemap>");
        //    }
        //    sb.AppendLine("</sitemapindex>");

        //    return File(Encoding.UTF8.GetBytes(sb.ToString()), "application/xml", "sitemap.xml");
        //}
    }
}
