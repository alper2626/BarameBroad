using System;
using BaseEntities.EnumTypes;
using Core.Attributes;
using DevExtreme.AspNet.Mvc;
using Extensions.UIExtensions.ControllerExtensions;
using Microsoft.AspNetCore.Mvc;

namespace BarameBroad.WebUI.Areas.CityTown.Controllers
{
    [Area("CityTown")]
    public class CityController : Controller
    {
        //private ICityService _cityService;

        //public CityController(ICityService cityService)
        //{
        //    _cityService = cityService;
        //}

        //[InRole(UserRole.Admin)]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[InRole(UserRole.Admin)]
        //public IActionResult GridSource(DataSourceLoadOptions loadOptions)
        //{
        //    var response = _cityService.GridSource<GetCityModel>(loadOptions);
        //    return Content(response.Data.ToString(), "application/json");
        //}

        //[InRole(UserRole.Admin)]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[InRole(UserRole.Admin)]
        //public IActionResult Update(Guid id)
        //{
        //    var response = _cityService.GetById<PostCityUpdateModel>(id);
        //    if (response.Success)
        //        return View(response.Data);

        //    return RedirectToAction("Index", "City", new { Area = "CityTown" }).Error(response.Message);

        //}

        //[HttpPost]
        //[InRole(UserRole.Admin)]
        //public IActionResult Create(PostCityCreateModel model)
        //{
        //    var response = _cityService.Create(model);

        //    if (!response.Success)
        //    {
        //        TempData["Error"] = response.Message;
        //        return View(model);
        //    }
                

        //    return RedirectToAction("Index", "City", new { area = "CityTown" }).Success(response.Message);
        //}

        //[HttpPost]
        //[InRole(UserRole.Admin)]
        //public IActionResult Update(PostCityUpdateModel model)
        //{
        //    var response = _cityService.Update(model);

        //    if (!response.Success)
        //    {
        //        TempData["Error"] = response.Message;
        //        return View(model);
        //    }

        //    return RedirectToAction("Index", "City", new { area = "CityTown" }).Success(response.Message);

        //}

        //[InRole(UserRole.Admin)]
        //public IActionResult Delete(Guid id)
        //{
        //    var response = _cityService.Delete(id);
        //    return Json(response);
        //}

        //[InRole(UserRole.Admin,UserRole.Customer)]
        //public IActionResult SelectBoxPartial()
        //{
        //    var response = _cityService.SelectBoxItems();
        //    if (response.Success)
        //        return PartialView("Partials/_SelectBoxPartial", response.Data);

        //    return PartialView("Partials/_ErrorPartial", response);
        //}

        //[InRole(UserRole.Admin, UserRole.Customer)]
        //public IActionResult SelectBoxAfterTownPartial()
        //{
        //    var response = _cityService.SelectBoxItems();
        //    if (response.Success)
        //        return PartialView("Partials/_SelectBoxAfterTownPartial", response.Data);

        //    return PartialView("Partials/_ErrorPartial", response);
        //}
    }
}