using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using CrossCuttingConcerns.Interceptors;
using Module.Auth.Business.Abstract;
using Module.Auth.Business.Concrete;
using Module.Auth.DataAccess.Abstract;
using Module.Auth.DataAccess.Concrete;


namespace Module.Auth.Business.Modules.AutoFac
{
    public class AuthAutoFacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<UserDal>().As<IUserDal>();

            builder.RegisterType<UserPasswordManager>().As<IUserPasswordService>();
            builder.RegisterType<UserPasswordDal>().As<IUserPasswordDal>();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<OperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<UserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<UserValidationCodeManager>().As<IUserValidationCodeService>();
            builder.RegisterType<UserValidationCodeDal>().As<IUserValidationCodeDal>();

            builder.RegisterType<NewsletterManager>().As<INewsletterService>();
            builder.RegisterType<NewsletterDal>().As<INewsletterDal>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}