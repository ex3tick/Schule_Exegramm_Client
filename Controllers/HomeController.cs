using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models.Interfaces;
using WebApp.Models.ModelClasses;
using WebApp.RestDal;


namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccessible _dal;
        private readonly BusinessLayer.BusinessLayer _businessLayer;


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

        public HomeController()
        {
            _businessLayer = new BusinessLayer.BusinessLayer();
            _dal = new RestDAL();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                getViewBags();

                List<Melder> melderList = await _dal.GetAllMelderAsync();
                List<Sichtung> sichtungenList = await _dal.GetAllSichtungAsync();
                List<DTOIndex> dtoList = new List<DTOIndex>();

                foreach (var melder in melderList)
                {
                    var dtoIndex = new DTOIndex { Melder = melder };

                    var sichtungenForMelder = sichtungenList.Where(s => s.MId == melder.MId).ToList();
                    if (sichtungenForMelder.Count > 0)
                    {
                        foreach (var sichtung in sichtungenForMelder)
                        {
                            var bilder = await _dal.GetBildBySichtungIdAsync(sichtung.SId) ?? new List<Bild>();
                            dtoIndex.Sichtungen.Add(new SichtungMitBilder { Sichtung = sichtung, Bilder = bilder });
                        }

                        dtoList.Add(dtoIndex);
                    }
                }

                return View(dtoList);
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HttpRequestException: {httpEx.Message}");
                return StatusCode(500, "ein Fehler ist aufgetreten");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "ein Fehler ist aufgetreten");
            }
        }





            [HttpGet]
        public async Task<IActionResult> Register()
        {
            getViewBags();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(Melder melder)
        {
            getViewBags();
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _businessLayer.RegisterMelder(melder))
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return View();
                }
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            getViewBags();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(DTOLoginMelder loginMelder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Melder melder = await _businessLayer.LoginMelder(loginMelder.Login, loginMelder.Kennwort);
                    if (melder != null)
                    {
                        HttpContext.Session.SetString("Melder", melder.Benutzername);
                        var BenutzerClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, melder.Benutzername),
                            new Claim(ClaimTypes.Email, melder.Email),
                            new Claim("MelderId", melder.MId.ToString())
                        };
                        if (melder.IsAdmin)
                        {
                            BenutzerClaims.Add(new Claim(ClaimTypes.Role, "Admin"));
                        }

                        ClaimsIdentity Ausweis = new ClaimsIdentity(BenutzerClaims, "BenuzterAusweis");
                        var BenutzerPrincipal = new ClaimsPrincipal(new[] { Ausweis });

                        await HttpContext.SignInAsync(BenutzerPrincipal, new AuthenticationProperties { });


                        return RedirectToAction("Index");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return View();
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("Melder");
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }
        
        public async Task<IActionResult> SichtungDetails(int id)
        {
            getViewBags();
            var sichtung = await _dal.GetSichtungByIdAsync(id);
            var bilder = await _dal.GetBildBySichtungIdAsync(id) ?? new List<Bild>();
            var sichtungMitBilder = new SichtungMitBilder { Sichtung = sichtung, Bilder = bilder };
            return View(sichtungMitBilder);
        }
    }
}