using TeacherUtilityBelt.Core.Abstractions;
using TeacherUtilityBelt.Infrastructure;
using TeacherUtilityBelt.Core;
using TeacherUtilityBelt.Core.Domain;
using React.AspNet;
using JavaScriptEngineSwitcher.V8;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;


namespace MyApp;

public class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        
        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .Build()
            ;

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddReact();

        // Make sure a JS engine is registered, or you will get an error!
        object value = builder.Services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName).AddV8();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var services = builder.Services;

        //adding console logging for now....
        services.AddLogging(builder => builder.AddConsole());
        services.Configure<AppSettings>(options => configurationBuilder.GetSection("Settings").Bind(options));                
        
        services.AddSingleton<ICacheManager, CacheManager>();

        services.AddTransient<IWordDictionary, WordDictionary>();
        services.AddTransient<IRequestManager, RequestManager>();
        services.AddTransient<IWordGridNavigator, WordGridNavigation>();
        services.AddTransient<IGridHelper, GridHelper>();
        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }



        app.UseHttpsRedirection();


        // Initialise ReactJS.NET. Must be before static files.
        app.UseReact(config =>
        {
        // If you want to use server-side rendering of React components,
        // add all the necessary JavaScript files here. This includes
        // your components as well as all of their dependencies.
        // See http://reactjs.net/ for more information. Example:
        //config
        config.AddScript("~/js/react/react.jsx");
        //  .AddScript("~/js/Second.jsx");

        // If you use an external build too (for example, Babel, Webpack,
        // Browserify or Gulp), you can improve performance by disabling
        // ReactJS.NET's version of Babel and loading the pre-transpiled
        // scripts. Example:
        //config
        //  .SetLoadBabel(false)
        //  .AddScriptWithoutTransform("~/js/bundle.server.js");
        });

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");


        app.Run();
    }
}