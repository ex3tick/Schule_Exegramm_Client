namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Konfiguration der Anwendung als MVC-Anwendung
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "SessionCookie";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = false;
            });

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            //Konfiguration der Session


            app.UseStaticFiles();

            app.UseRouting();

            //Konfiguration der Session
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}