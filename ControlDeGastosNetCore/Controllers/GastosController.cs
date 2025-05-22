using ControlDeGastosNetCore.Models;
using ControlDeGastosNetCore.Services;
using Microsoft.AspNetCore.Mvc;

public class GastosController : Controller
{
    private readonly IGastoService _gastoService;

    public GastosController(IGastoService gastoService)
    {
        _gastoService = gastoService;
    }

    public async Task<IActionResult> Index(int? mes, int? anio)
    {
        var resumen = await _gastoService.ObtenerResumenDelMes(mes, anio);
        return View(resumen);
    }

    public IActionResult Create()
    {
        // Ejemplo simplificado
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Gasto gasto)
    {
        if (ModelState.IsValid)
        {
            await _gastoService.CrearGastoAsync(gasto);
            return RedirectToAction(nameof(Index));
        }
        return View(gasto);
    }
}

