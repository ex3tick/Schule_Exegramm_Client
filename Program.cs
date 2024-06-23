using Microsoft.AspNetCore.DataProtection;
using System.IO;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "SessionCookie";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddAuthentication("CookieAuthentifizierung").AddCookie("CookieAuthentifizierung", Konf =>
            {
                Konf.Cookie.Name = "UserLoginCookie";
                Konf.LoginPath = "/home/Login";
                Konf.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                //Festlegung des Umleitungsparameter. Standardm��ig ist dies "ReturnUrl".
                Konf.ReturnUrlParameter = "Umleitung";
                //Festlegung des Pfads f�r den Zugriff verweigert. (F�r angemeldete User ohne erweiterte Rechte)
                Konf.AccessDeniedPath = "/home/AccessDenied";
            });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
