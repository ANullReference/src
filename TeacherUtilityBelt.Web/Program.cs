using TeacherUtilityBelt.Core;
using TeacherUtilityBelt.Core.Abstractions;
using TeacherUtilityBelt.Core.Domain;
using TeacherUtilityBelt.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

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


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();