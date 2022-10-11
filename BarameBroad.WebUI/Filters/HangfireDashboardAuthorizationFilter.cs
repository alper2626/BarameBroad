using System.Linq;
using System.Security.Claims;
using BaseEntities.EnumTypes;
using Hangfire.Dashboard;

namespace BarameBroad.WebUI.Filters
{
    //Burası Role based değil Sistemde rol tabanlı birşeye ihtiyacım yok
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            var roles = httpContext.User.Claims.FirstOrDefault(q => q.Type == ClaimTypes.Role);
            if (roles == null)
                return false;

            if (roles.Value.Contains(UserRole.Admin.ToString()))
                return true;

            return false;
        }
    }
}