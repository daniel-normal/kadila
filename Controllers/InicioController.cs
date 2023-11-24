using Microsoft.AspNetCore.Mvc;

namespace Inicio.Controllers
{
    public class InicioController : Controller
    {
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
