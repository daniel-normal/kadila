using kadila.Data;
using Microsoft.AspNetCore.Mvc;
using kadila.Models;
namespace Inicio.Controllers
{
    public class InicioController : Controller
    {
        private readonly DotnetContext _context;
        
        public InicioController(DotnetContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public IActionResult Contact(Contact model)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Add(model);
                _context.SaveChanges();
                string name = model.Name;
                TempData["SuccessMessage"] = $"Felicidades {name}, tu solicitud se envió exitosamente.";
                return RedirectToAction("Contact");
            }
            return View(model);
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        
        public IActionResult Portfolio()
        {
            return View();
        }
        
        public IActionResult Services()
        {
            return View();
        }
        
        public IActionResult Contact()
        {
            return View();
        }
    }
}
