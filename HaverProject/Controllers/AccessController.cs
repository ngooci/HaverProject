using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using HaverProject.Models;
using Microsoft.AspNetCore.Identity;

namespace HaverProject.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Dashboard", "NCR");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin)
        {
            // Check if the provided email and password match any user
            if ((modelLogin.Emails == "admin@company.com" && modelLogin.Password == "AdminPass") ||
                (modelLogin.Emails == "quality@company.com" && modelLogin.Password == "QualityPass") ||
                (modelLogin.Emails == "engineering@company.com" && modelLogin.Password == "EngineeringPass") ||
                (modelLogin.Emails == "purchase@company.com" && modelLogin.Password == "PurchasePass") ||
                (modelLogin.Emails == "review@company.com" && modelLogin.Password == "ReviewPass"))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Emails),
                };

                // Assign roles based on email addresses
                if (modelLogin.Emails == "admin@company.com")
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                else if (modelLogin.Emails == "quality@company.com")
                {
                    claims.Add(new Claim(ClaimTypes.Role, "QualityInspector"));
                }
                else if (modelLogin.Emails == "engineering@company.com")
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Engineering"));
                }
                else if (modelLogin.Emails == "purchase@company.com")
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Purchase"));
                }
                else if (modelLogin.Emails == "review@company.com")
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Review"));
                }

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Dashboard", "NCR");
            }

            ViewData["ValidateMessage"] = "User not found or incorrect password";
            return View(); // Return the login view with model if login fails
        }
    }
}
