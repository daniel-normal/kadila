using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using kadila.Models;
using kadila.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NETCore.MailKit.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using MimeKit;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace kadila.Controllers
{
    public class LoginController : Controller
    {
        private readonly DotnetContext _context;
        private readonly NETCore.MailKit.Core.IEmailService _EmailService;

        public LoginController(DotnetContext context, NETCore.MailKit.Core.IEmailService emailService)
        {
            _context = context;
            _EmailService = emailService;
        }
        
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Inicio");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult EmailVerification()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> EmailVerification(string email)
        {
            var resetToken = Guid.NewGuid().ToString();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.ResetToken = resetToken;
                user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1);
                await _context.SaveChangesAsync();
                var resetLink = Url.Action("ResetPassword", "Login", new { email, token = resetToken }, protocol: HttpContext.Request.Scheme);
                _EmailService.Send(email, "Restablecer contraseña", $"Haz clic en el enlace para restablecer tu contraseña: {resetLink}");
            }
            TempData["SuccessMessage"] = "Revisa tu correo electrónico para cambiar tu contraseña.";
            return RedirectToAction("EmailVerification");
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            var user = await _context.Users
                .Where(u => u.Email == email && u.ResetToken == token)
                .FirstOrDefaultAsync();
            if (user != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(User model)
        {
            try
            {
                var user = await _context.Users
                    .Where(u => u.Email == model.Email)
                    .FirstOrDefaultAsync();
                var newPasswordHash = new PasswordHasher<User>().HashPassword(user, model.Password);
                user.Password = newPasswordHash;
                user.ResetToken = null;
                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Login");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Se produjo un error al restablecer la contraseña.");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user, bool rememberMe)
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

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = rememberMe,
                        ExpiresUtc = DateTime.UtcNow.AddHours(5)
                        //ExpiresUtc = DateTime.UtcNow.AddSeconds(5)
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);

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
            catch
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