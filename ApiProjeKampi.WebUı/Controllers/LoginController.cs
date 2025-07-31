using Microsoft.AspNetCore.Mvc;
using ApiProjeKampi.WebUı.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace ApiProjeKampi.WebUı.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            // Burada gerçek kimlik doğrulama yapılmalı (veritabanı vs.)
            if (username == "admin" && password == "1234")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin") // Rol burada belirleniyor
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // tarayıcı kapansa da açık kalsın mı?
                };

                await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Error = "Hatalı kullanıcı adı veya şifre.";
            return View();
        }

        // Çıkış yapma
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // Erişim engellendi
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
