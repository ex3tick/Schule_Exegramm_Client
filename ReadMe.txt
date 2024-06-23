Von leerer Web-App zu vollständiger Web-App
-Verzeichnesstruktur anlegen
--Controllers
--Models
--Views

-Controller anlegen (z.B. HomeController)
-View anlegen (z.B. Index.cshtml)

- Web APP als MVC Anwendung festlegen
-- builder.Services.AddControllersWithViews();

- Routing aktivieren
-- app.UseRouting();

-Standartroute festlegen
--app.MapControllerRoute(name: "default",pattern: "{controller=Home}/{action=Index}/{id?}");