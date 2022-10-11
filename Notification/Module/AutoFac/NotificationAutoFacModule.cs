using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using CrossCuttingConcerns.Interceptors;
using Notification.Abstract;
using Notification.Concrete;
using Notification.Helpers;

namespace Notification.Module.AutoFac
{
    public class NotificationAutoFacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MailingManager>().As<IMailingService>();
            builder.RegisterType<MailTemplateHelper>().As<IMailTemplateHelper>();
            
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}