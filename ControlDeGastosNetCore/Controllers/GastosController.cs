using ControlDeGastosNetCore.Models;
using ControlDeGastosNetCore.Services;
using ControlDeGastosNetCore.Viewmodels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class GastosController : Controller
{
    private readonly IGastoService _gastoService;
    private readonly ICategoriaService _categoriaService;

    public GastosController(IGastoService gastoService, ICategoriaService categoriaService)
    {
        _gastoService = gastoService;
        _categoriaService = categoriaService;
    }

    public async Task<IActionResult> Resumen(int? mes, int? anio)
    {
        var resumen = await _gastoService.ObtenerResumenDelMes(mes, anio);
        return View(resumen);
    }

    public IActionResult Create()
    {
        var model = new GastoViewmodel
        {
            Fecha = DateTime.Now,
            Categorias = _categoriaService.GetAll()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GastoViewmodel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categorias = _categoriaService.GetAll();
            return View(model);
        }

        var gasto = new Gasto
        {
            Monto = model.Monto,
            Fecha = model.Fecha,
            Detalle = model.Detalle,
            CategoriaId = model.CategoriaId
        };

        await _gastoService.CrearGastoAsync(gasto); 
        return RedirectToAction(nameof(Resumen)); 
    }

}

