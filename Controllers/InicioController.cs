using Microsoft.AspNetCore.Mvc;

namespace Inicio.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
