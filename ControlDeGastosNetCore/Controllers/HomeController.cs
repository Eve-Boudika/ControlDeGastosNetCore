using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlDeGastosNetCore.Models; // Asegúrate que la ruta sea correcta

namespace ControlDeGastosNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Acción para la página principal
        public IActionResult Index()
        {
            return View();
        }

        // Acción para la página de privacidad
        public IActionResult Privacy()
        {
            return View();
        }

        // Acción para la página de error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            _logger.LogError("Se ha producido un error con RequestId: {RequestId}", errorModel.RequestId);

            return View(errorModel);
        }
    }
}
