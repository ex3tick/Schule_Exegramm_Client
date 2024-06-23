using Microsoft.AspNetCore.DataProtection;
using System.IO;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Konfiguration der Anwendung als MVC-Anwendung
            builder.Services.AddControllersWithViews();

            // Session-Konfiguration
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "SessionCookie";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });

            // Persistente Speicherung der Schlüssel
            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"./keys/"))
                .SetApplicationName("YourApplicationName");

            var app = builder.Build();

            // Middleware-Konfiguration
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}