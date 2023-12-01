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
            return RedirectToAction("Index", "Inicio");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
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
                    var session = new Session
                    {
                        LoginDate = DateTime.Now,
                        UserId = validatedUser.Id
                    };
                    _context.Add(session);
                    await _context.SaveChangesAsync();

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



        public async Task<IActionResult> Logout(ulong id, [Bind("Id,UserId")] Session session)
        {
            if (id != session.Id)
            {
                return RedirectToAction("Index", "Inicio");
            }
            var existingSession = await _context.Sessions.FindAsync(id);
            if (existingSession != null)
            {
                existingSession.LogoutDate = DateTime.Now;
                _context.Update(existingSession);
                await _context.SaveChangesAsync();
            }
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Inicio");
        }
    }
}