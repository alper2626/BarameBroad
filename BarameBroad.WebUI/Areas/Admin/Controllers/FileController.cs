using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using BaseEntities.EnumTypes;
using BaseEntities.Response;
using Core.Attributes;
using Extensions.UiExtensions;
using Extensions.UIExtensions.ControllerExtensions;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Net;

namespace BarameBroad.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [InRole(UserRole.Admin)]
    public class FileController : Controller
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public FileController(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _env = env;
        }

        public IActionResult FilePartial(Guid? itemId, string type)
        {
            TempData["FileType"] = type;
            return PartialView("_FilePartial", StaticFileOperations.FilePaths(itemId, type));
        }

        public IActionResult FileBasePartial(string type)
        {
            return PartialView("_FilePartial", StaticFileOperations.FilePaths(type));
        }

        public ActionResult DownloadFile(string path)
        {
            try
            {
                path = Path.Combine(_env.ContentRootPath, "wwwroot", path.Substring(1, path.Length - 1));
                if (System.IO.File.Exists(path))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, path.Split('/').Last());
                }
                return RedirectToAction("Error", "Home").Error("Dosyalar İndirilemedi.");


            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home").Error("Dosyalar İndirilemedi.");
            }
        }

        public IActionResult DeleteFile(string path)
        {
            var response = new DataResponse
            {
                Success = true,
                Message = "Dosya Silindi",
            };
            if (!StaticFileOperations.DeleteFile(path))
            {
                response.Success = false;
                response.Message = "Dosya Silinirken Hata Oluştu";
            }

            return Json(response);

        }

        [HttpPost]
        public IActionResult MoveItems(List<string> paths)
        {
            var pathBase = Path.Combine(_env.ContentRootPath, "wwwroot");
            var res = StaticFileOperations.ReOrderItems(paths,pathBase);
            if (res.Count > 0)
                return Json(new DataResponse
                {
                    Data = res,
                    Message = "Sıralama Değiştirildi",
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                });

            return Json(new DataResponse
            {
                Data = res,
                Message = "Sıralama Değiştirilirken Hata Oluştu",
                StatusCode = HttpStatusCode.BadRequest,
                Success = false
            });
        }
    }
}
