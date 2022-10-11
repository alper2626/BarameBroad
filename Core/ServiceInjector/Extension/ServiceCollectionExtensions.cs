using Core.ServiceInjector.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.ServiceInjector.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services,
            ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(services);
            }

            return ServiceTool.Create(services);
        }

    }
}
