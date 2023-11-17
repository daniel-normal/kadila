using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using kadila.Models;
using kadila.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace kadila.Controllers
{
    public class LoginController : Controller
    {
        private readonly DotnetContext _context;

        public LoginController(DotnetContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            try
            {
                var validatedUser = _context.ValidateUser(user.Email, user.Password);

                if (validatedUser != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, validatedUser.Name),
                    new Claim("Email", validatedUser.Email)
                };

                    if (validatedUser.RolId.HasValue)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, validatedUser.RolId.ToString()));
                    }

                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("Email", "Correo electrónico o contraseña incorrectos.");
                return View();
            }
            catch(Exception e)
            {
                ModelState.AddModelError("Email", "Correo electrónico o contraseña incorrectos.");
                return View();
            }
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}