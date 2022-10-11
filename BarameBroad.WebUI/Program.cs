using Autofac;
using Autofac.Extensions.DependencyInjection;
using BarameBroad.WebUI.Filters;
using BarameBroad.WebUI.Localizer;
using BarameBroad.WebUI.Middlewares;
using Core.Modules.AutoFac;
using Core.ServiceInjector.DependencyResolvers;
using Core.ServiceInjector.Extension;
using Core.ServiceInjector.Utilities.IoC;
using Extensions.UiExtensions;
using Extensions.UiExtensions.MenuExtensions;
using Extensions.UiExtensions.MenuExtensions.NavMenuHelper;
using Hangfire;
using Hangfire.SqlServer;
using MemCaching.Modules.AutoFac;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using Module.Auth.Business.Modules.AutoFac;
using Notification.Module.AutoFac;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new CoreAutoFacModule());
    builder.RegisterModule(new AuthAutoFacModule());
    builder.RegisterModule(new CacheAutoFacModule());
    builder.RegisterModule(new NotificationAutoFacModule());
});


builder.Services.AddLocalization(options =>
{
    // Resource (kaynak) dosyalarýmýzý ana dizin altýnda "Resources" klasorü içerisinde tutacaðýmýzý belirtiyoruz.
    options.ResourcesPath = "Resources";
});
// Add builder.Services to the container.

builder.Services.AddControllersWithViews((options) =>
{
    options.ModelBinderProviders.Insert(0, new CustomBinderProvider());
})
    .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
    .AddDataAnnotationsLocalization()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
}).AddRazorRuntimeCompilation();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential 
    // cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    // requires using Microsoft.AspNetCore.Http;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});




builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    CookieAuthenticationDefaults.AuthenticationScheme, config =>
    {
        config.Cookie.Name = "baremabroad.Authentication";
        config.SlidingExpiration = true;
        config.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        config.AccessDeniedPath = new PathString("/hesap/giris");
        config.LoginPath = new PathString("/hesap/giris");
    });

builder.Services.ConfigureNonBreakingSameSiteCookies();

CultureInfo[] cultures = new CultureInfo[]
{
        new("tr-TR"),
        new("en-US")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new("tr-TR");
    //options.DefaultRequestCulture = new("en-US");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});



builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });

builder.Services.AddScoped<RequestLocalizationCookiesMiddleware>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

builder.Services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();






builder.Services.AddHangfire(x =>
    x.UseSqlServerStorage(
            builder.Configuration.GetSection("SqlConnectionString").Get<string>(),
            new SqlServerStorageOptions
            {
                PrepareSchemaIfNecessary = true,
                QueuePollInterval = TimeSpan.FromMinutes(5),
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true
            })
        .WithJobExpirationTimeout(TimeSpan.FromHours(12)));
builder.Services.AddHangfireServer();


builder.Services.AddMemoryCache();

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
                new CoreModule(),
});

var d = AppDomain.CurrentDomain.GetAssemblies();
builder.Services.RegisterAllTypes<INavMenuBuild>(d);


builder.Services.Configure<CookieTempDataProviderOptions>(options => {
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();



var localizationOptions = new RequestLocalizationOptions
{
    SupportedCultures = cultures,
    SupportedUICultures = cultures,
    //DefaultRequestCulture = new RequestCulture("tr-TR"),
    DefaultRequestCulture = new RequestCulture("en-US"),
};
var requestProvider = new RouteDataRequestCultureProvider();
localizationOptions.RequestCultureProviders.Insert(0, requestProvider);

app.UseRequestLocalization(localizationOptions);
app.UseRequestLocalizationCookies();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseHangfireDashboard("/admin/hangfirejobs", new DashboardOptions
{
    Authorization = new[] { new HangfireDashboardAuthorizationFilter() }
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

});
NavMenuWebHostExtensions.RunWithTasks(app.Services);

app.Run();
