using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BaseEntities.EnumTypes;
using Core.Attributes;
using Extensions.UiExtensions;
using Extensions.UIExtensions.ControllerExtensions;

namespace BarameBroad.WebUI.Areas.SiteUserSection.Controllers
{
    [Area("SiteUserSection")]
    [InRole(UserRole.Admin)]
    public class SiteSectionController : Controller
    {
        //private ISiteSectionService _siteSectionService;

        //public SiteSectionController(ISiteSectionService siteSectionService)
        //{
        //    _siteSectionService = siteSectionService;
        //}

        //public IActionResult Update(Guid id)
        //{
        //    var res = _siteSectionService.Get<PostSiteSectionUpdateModel>(id);
        //    if (res.Success)
        //        return View(res.Data);
        //    return RedirectToAction("Index", "Home",new{area="Admin"}).Error(res.Message);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Update(PostSiteSectionUpdateModel model)
        //{
        //    var response = _siteSectionService.Update(model);

        //    if (!response.Success)
        //    {
        //        if (Request.IsAjaxRequest())
        //            return Json(response);
        //        return View(response.Data).Error(response.Message);
        //    }

        //    var res = (PostSiteSectionUpdateModel) response.Data;

        //    var imageResponse = await StaticFileOperations.UploadFiles("SiteSection", res.Id, Request.Form.Files);
        //    if (Request.IsAjaxRequest())
        //        return Json(response);

        //    if (imageResponse)
        //    {
        //        TempData["Success"] = response.Message;
        //        return View(response.Data);
        //    }

        //    TempData["Error"] = response.Message;
        //    return View(response.Data);
        //}

        //public IActionResult MenuPartial()
        //{
        //    var res = _siteSectionService.SelectBoxSiteSection();
        //    if (res.Success)
        //        return PartialView("Partials/_SiteSectionMenuItemsPartial",res.Data);

        //    return PartialView("Partials/_ErrorPartial", res);
        //}
    }
}
