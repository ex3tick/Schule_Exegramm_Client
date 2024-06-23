using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Interfaces;
using WebApp.Models.ModelClasses;
using WebApp.RestDal;


namespace WebApp.Controllers
{
    
    
    
    public class HomeController : Controller
    {
        private readonly IAccessible _dal;
        private readonly BusinessLayer.BusinessLayer _businessLayer;
        
        public HomeController()
        {
            _businessLayer = new BusinessLayer.BusinessLayer();
            _dal = new RestDAL();
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            return View();
        }
        [HttpGet]
        public async  Task<IActionResult> Register()
        {
            
            return View();
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Register(Melder melder)
        {
            if(ModelState.IsValid)
            {
                try
                {
                   if(await _businessLayer.RegisterMelder(melder))
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string login, string kennwort)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Melder melder = await _businessLayer.LoginMelder(login, kennwort);
                    if (melder != null)
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
        
        
        
    }
}