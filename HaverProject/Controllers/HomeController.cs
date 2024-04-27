using HaverProject.Data;
using HaverProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using MailKit;
namespace HaverProject.Controllers
{

    [Authorize]
 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HaverContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

       
        public IActionResult Index()
        {
            // Redirect to login if not authenticated
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }

            // Redirect to dashboard if authenticated
            return RedirectToAction("Dashboard", "NCR");

        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task< IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
          
        }

        [HttpGet("Dashboard")]
        [Authorize] 
        public IActionResult Dashboard()
        {
            return View("Dashboard");
        }
        //[HttpGet("Ncr")]
        //public IActionResult Ncr()
        //{
        //    var ncr = _context.Ncrs.AsNoTracking();
        //    return View("Index", ncr);
        //}
        [HttpGet("Email")]
        public IActionResult Email()
        {
            return View("Index");
        }
    }
}