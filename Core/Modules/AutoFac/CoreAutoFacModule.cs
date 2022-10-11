using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Core;
using Core.DataAccess.Abstract;
using Core.DataAccess.Concrete;
using Core.LogModule;
using Core.WebHelper;
using CrossCuttingConcerns.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Core.Modules.AutoFac
{
    public class CoreAutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomDbContext>().As<DbContext>();
            builder.RegisterType<BaseLogDal>().As<IBaseLogDal>();
            builder.RegisterGeneric(typeof(QueryableRepositoryBase<>)).As(typeof(IQueryableRepositoryBase<>));
            builder.RegisterGeneric(typeof(EntityRepositoryBase<>)).As(typeof(IEntityRepositoryBase<>));
            builder.RegisterGeneric(typeof(EntityRepositoryBase<,>)).As(typeof(IEntityRepositoryBase<,>));
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}