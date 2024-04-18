using Microsoft.AspNetCore.Authentication.Cookies;
using TrollMarketWeb.Configurations;
using static TrollMarketDataAccess.Dependencies;

namespace TrollMarketWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IServiceCollection services = builder.Services;
        services.AddControllersWithViews();
        services.AddBusinessServices();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options=> {
            options.Cookie.Name = "AuthCookie";
            options.LoginPath = "/Login";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.AccessDeniedPath = "/AccessDenied";
        });
        services.AddAuthorization();

        ConfigureService(builder.Configuration, builder.Services);

        var app = builder.Build();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Auth}/{action=Index}"
        );

        app.UseStaticFiles();

        app.Run();
    }
}
