using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Core.ServiceInjector.Utilities.IoC;
using Core.WebHelper;
using Microsoft.AspNetCore.Http;

namespace Core.ServiceInjector.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IClaimWebHelper, ClaimWebHelper>();
            services.AddSingleton<Stopwatch>();

        }
    }
}
