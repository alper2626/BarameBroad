using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Core.Attributes
{
    public class IsAuthenticateAttribute : ActionFilterAttribute
    {
        
        public IsAuthenticateAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                ITempDataDictionaryFactory factory = context.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
                ITempDataDictionary tempData = factory.GetTempData(context.HttpContext);
                tempData["Error"] = "Lütfen Giriş Yapın.";
                if (context.HttpContext.Request.Path.ToString().ToLower().Contains("admin"))
                    context.Result = new RedirectToActionResult("Login", "Account", new { Area = "Auth" });
                else
                    context.Result = new RedirectToActionResult("Login", "Auth", new { Area = "" });
            }
        }

    }
}