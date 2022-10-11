using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BaseEntities.Abstract;
using BaseEntities.EnumTypes;
using BaseEntities.Response;
using Core.ServiceInjector.Utilities.IoC;
using Core.WebHelper;
using Extensions.HttpRequestExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Core.Attributes
{
    public class BySelfOrInRoleAttribute : ActionFilterAttribute
    {
        private IClaimWebHelper _claimWebHelper;

        private UserRole[] _assignableRoles;
        public BySelfOrInRoleAttribute(params UserRole[] assignableRoles)
        {
            _assignableRoles = assignableRoles;
            _claimWebHelper = (IClaimWebHelper)ServiceTool.ServiceProvider.GetService(typeof(IClaimWebHelper));
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.Any(q => q.Value is IUserAssignableModel))
                throw new Exception("Parametreler Geçerli Değil.");

            var entity = context.ActionArguments.Where(q => q.Value is IUserAssignableModel)
                .Select(w => w.Value as IUserAssignableModel).FirstOrDefault();

            if (entity == null)
                throw new Exception("Parametreler Geçerli Değil.");



            if (entity.UserId == _claimWebHelper.Id)
                return;

            if (_claimWebHelper.IsInRole(_assignableRoles.ToList()))
                return;

            if (context.HttpContext.Request.IsAjaxRequest())
            {
                DataResponse res = new DataResponse
                {
                    Success = false,
                    Message = "Bu işlem için yetkiniz yok.",
                    StatusCode = HttpStatusCode.BadRequest
                };

                context.Result = new JsonResult(res);
            }
            else
            {
                ITempDataDictionaryFactory factory = context.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
                ITempDataDictionary tempData = factory.GetTempData(context.HttpContext);
                tempData["Error"] = "Bu işlem için yetkiniz yok.";
                if (context.HttpContext.Request.Path.ToString().ToLower().Contains("admin"))
                    context.Result = new RedirectToActionResult("Login", "Account", new { Area = "Auth" });
                else
                    context.Result = new RedirectToActionResult("Login", "Auth", new { Area = "" });
            }

            

        }

    }
}