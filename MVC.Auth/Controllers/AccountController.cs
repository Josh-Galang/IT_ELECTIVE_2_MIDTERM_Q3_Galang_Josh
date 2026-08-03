using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVC.Auth.Data;
using MVC.Auth.Models;
using System.Security.Claims;

namespace MVC.Auth.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            //check if the user exists in the database
            var user = FakeDbContext.Users.FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);

            //if user is null, return to login page with error message
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");

                return View(model);
            }

            // create claims for the user, these claims will be stored in the authentication cookie
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Country, "Philippines"),
                new Claim("Course","BSIT"),
            };

            // create a claims identity with the claims and the authentication scheme
            
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // create a claims principal with the claims identity
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Home");

        }
    }
}
