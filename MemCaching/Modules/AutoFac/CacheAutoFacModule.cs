using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using CrossCuttingConcerns.Interceptors;
using MemCaching.Abstract;
using MemCaching.Concrete;
using Microsoft.Extensions.Caching.Memory;

namespace MemCaching.Modules.AutoFac
{
    public class CacheAutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MemoryCacheManager>().As<ICacheService>();
            builder.RegisterType<MemoryCache>().As<IMemoryCache>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}