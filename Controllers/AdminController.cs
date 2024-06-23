using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            getViewBags();
            return View();
        }

        public void getViewBags()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Melder = User.Identity.Name;
                ViewBag.Email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                ViewBag.IsAdmin = User.IsInRole("Admin");
                ViewBag.MelderId = User.Claims.FirstOrDefault(c => c.Type == "MelderId")?.Value;
            }
            else
            {
                ViewBag.Melder = null;
                ViewBag.Email = null;
                ViewBag.IsAdmin = false;
                ViewBag.MelderId = null;
            }
        }

        public IActionResult Admin()
        {
            getViewBags();
            return View();
        }
    }
}