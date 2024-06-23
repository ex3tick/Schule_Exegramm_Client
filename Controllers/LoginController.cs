using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Interfaces;
using WebApp.Models.ModelClasses;
using WebApp.RestDal;

namespace WebApp.Controllers;

[Authorize]
public class LoginController : Controller
{
    
    private readonly IAccessible _dal;
    private readonly BusinessLayer.BusinessLayer _businessLayer;


    public LoginController()
    {
        _businessLayer = new BusinessLayer.BusinessLayer();
        _dal = new RestDAL( );
    }
    
    public IActionResult Index()
    {
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
    

    public async Task<IActionResult>Profil()
    {
        getViewBags();
        int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "MelderId")?.Value);
        var melder = await _dal.GetMelderByIdAsync(id) ;
        return View(melder);
    }
    
    [HttpGet]
    public async Task<IActionResult> UploadSichtung()
    {
        getViewBags();
        DTOSichtungUplaod dto = new DTOSichtungUplaod();
        dto.Sichtung.MId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "MelderId")?.Value);
        dto.Kategorie = await _dal.GetAllKategorieAsync();
        
        return View(dto);
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadSichtung(DTOSichtungUplaod dto)
    {
       if(!ModelState.IsValid) return View(dto);

        getViewBags();
        int mId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "MelderId")?.Value);
          dto.Sichtung.MId = mId;
          dto.Sichtung.KId = 2;
       int sId = await _dal.AddSichtungAsync(dto.Sichtung);
       
       

        if (dto.File == null || dto.File.Count == 0)
        {
            ModelState.AddModelError("File", "Bitte wählen Sie ein Bild aus");
            return View(dto);
        }

        foreach (var file in dto.File)
        {
            if (file == null || file.Length == 0 || !file.ContentType.Contains("image"))
            {
                ModelState.AddModelError("File", "Bitte wählen Sie ein Bild aus");
                return View(dto);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", uniqueFileName);
            dto.Bild.SId = sId;
            dto.Bild.Name = uniqueFileName;
            await _dal.AddBildAsync(dto.Bild);
            
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        return RedirectToAction("Profil"); 
    }

}