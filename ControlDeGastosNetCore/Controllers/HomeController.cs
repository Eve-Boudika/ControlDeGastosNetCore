using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControlDeGastosNetCore.Models; // Aseg�rate que la ruta sea correcta

namespace ControlDeGastosNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Acci�n para la p�gina principal
        public IActionResult Index()
        {
            return View();
        }

        // Acci�n para la p�gina de privacidad
        public IActionResult Privacy()
        {
            return View();
        }

        // Acci�n para la p�gina de error
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
