using BarameBroad.WebUI.Base;
using BarameBroad.WebUI.Models;
using BaseEntities.Database;
using Core.DataAccess.Abstract;
using Extensions.UIExtensions.ControllerExtensions;
using MemCaching.Abstract;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Notification.Abstract;
using Notification.Entities.Concrete;

namespace BarameBroad.WebUI.Controllers
{
    [Route("hakkimizda")]
    public class SiteConfigController : UserController<MasterSiteConfig>
    {
        IMailingService _mailingService;
        IEntityRepositoryBase<SocialNetworkEntity> _socialRepo;
        public SiteConfigController(IEntityRepositoryBase<MasterSiteConfig> repo, ICacheService cacheService, IHttpContextAccessor httpContextAccessor, IMailingService mailingService, IEntityRepositoryBase<SocialNetworkEntity> socialRepo) : base(repo, cacheService, httpContextAccessor)
        {
            _mailingService = mailingService;
            _socialRepo = socialRepo;
        }

        [Route("")]
        public IActionResult Index()
        {
            var socials = CacheService.Get<List<SocialNetworkEntity>>("SocialNetworks");

            if (socials == null)
            {
                socials = _socialRepo.GetList().ToList();
                CacheService.AddOrUpdate("SocialNetworks", socials);
            }

            ViewData["Socials"] = socials;

            var config = CacheService.Get<MasterSiteConfig>("SiteConfig");
            if (config == null)
            {
                config = Repository.First(new System.Linq.Expressions.Expression<Func<MasterSiteConfig, object>>[] { w => w.Childrens });
                CacheService.AddOrUpdate("SiteConfig", config);
            }


            return View(config.Childrens.First(w => w.Lang == UserLang));
        }

        [Route("anasayfa")]
        public IActionResult HomePartial()
        {
            var config = CacheService.Get<MasterSiteConfig>("SiteConfig");
            if (config == null)
            {
                config = Repository.First(new System.Linq.Expressions.Expression<Func<MasterSiteConfig, object>>[] { w => w.Childrens });
                CacheService.AddOrUpdate("SiteConfig", config);
            }
            var data = config.Childrens.First(w => w.Lang == UserLang);
            return PartialView("Partials/_HomePartial", data);
        }

        [Route("stat")]
        public IActionResult StatPartial()
        {
            var config = CacheService.Get<MasterSiteConfig>("SiteConfig");
            if (config == null)
            {
                config = Repository.First(new System.Linq.Expressions.Expression<Func<MasterSiteConfig, object>>[] { w => w.Childrens });
                CacheService.AddOrUpdate("SiteConfig", config);
            }
            return PartialView("Partials/_StatPartial", config);
        }


        [Route("iletisim")]
        public IActionResult Contact()
        {
            var config = CacheService.Get<MasterSiteConfig>("SiteConfig");
            if (config == null)
            {
                config = Repository.First(new System.Linq.Expressions.Expression<Func<MasterSiteConfig, object>>[] { w => w.Childrens });
                CacheService.AddOrUpdate("SiteConfig", config);
            }
            ViewData["Config"] = config.Childrens.First(w => w.Lang == UserLang);
            return View();
        }

        [Route("iletisimpartial")]
        public IActionResult ContactPartial()
        {

            return PartialView("Partials/_ContactPartial");
        }

        [Route("sitemailgonder")]
        public IActionResult SendMail(PostContactForm model)
        {
            var mail = new SendMailBaseModel
            {
                Content = model.Message,
                IsContactMail = true,
                MailModel = model,
                Subject = "Siteden Mail Var !!!",
                TemplateName = "_SiteContactMail"
            };
            _mailingService.Send(mail);
            return RedirectToAction("Index", "Home").Success("Mailinize En Kısa Sürede Dönüş Yapılacaktır.");
        }

        private bool CheckCaptcha()
        {
            var postData = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("secret", "secrettttt"),
                new KeyValuePair<string, string>("remoteip", HttpContext.Connection.RemoteIpAddress.ToString()),
                new KeyValuePair<string, string>("response", HttpContext.Request.Form["g-recaptcha-response"])
            };

            var client = new HttpClient();
            var response = client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData)).Result;

            var o = (JObject)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

            return (bool)o["success"];
        }

        [Route("dildegistir")]
        public IActionResult ChangeLang(string lang)
        {

            Response.Cookies.Append(".AspNetCore.Culture", CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)));
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
