using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Auth.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace MVC.Auth.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.User.Identity;

            var country = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Country)?.Value;

            if(country == "Philippines")
            {
                return RedirectToAction("Privacy");
            }

            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var user = HttpContext.User.Identity;

            ViewBag.WelcomeScript = $"Welcome back to the Philippines {user?.Name}!";
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
