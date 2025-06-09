using Microsoft.AspNetCore.Mvc;
using ControlDeGastosAPI.Models;
using ControlDeGastosAPI.Services;
using ControlDeGastosAPI.DTOS;

namespace ControlDeGastosAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GastosController : ControllerBase
{
    private readonly IGastoService _gastoService;

    public GastosController(IGastoService gastoService)
    {
        _gastoService = gastoService;
    }

    // GET: api/gastos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Gasto>>> GetGastos()
    {
        var gastos = await _gastoService.GetAllAsync();
        return Ok(gastos);
    }

    // GET: api/gastos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GastoDeleteDTORes>> GetGasto(int id)
    {
        Gasto gasto = await _gastoService.GetByIdAsync(id);

        GastoDeleteDTORes gastoDeleteDTORes = new GastoDeleteDTORes
        {
            Id = gasto.Id,
            Monto = gasto.Monto,
            Fecha = gasto.Fecha,
            Detalle = gasto.Detalle,
            CategoriaId = gasto.CategoriaId
        };


        if (gasto == null)
            return NotFound();

        return Ok(gastoDeleteDTORes);
    }

    // POST: api/gastos
    [HttpPost]
    public async Task<GastoPostDTORes> CreateGasto(GastoPostDTOReq gastoPostDTOReq)
    {
        Gasto gasto = new Gasto
        {
            Monto = gastoPostDTOReq.Monto,
            Fecha = gastoPostDTOReq.Fecha,
            Detalle = gastoPostDTOReq.Detalle,
            CategoriaId = gastoPostDTOReq.CategoriaId
        };

        return await _gastoService.CreateAsync(gasto);
    }

    // PUT: api/gastos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGasto(int id, Gasto gasto)
    {
        if (id != gasto.Id)
            return BadRequest();

        var existingGasto = await _gastoService.GetByIdAsync(id);
        if (existingGasto == null)
            return NotFound();

        await _gastoService.UpdateAsync(gasto);
        return NoContent();
    }

    // DELETE: api/gastos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGasto(int id)
    {
        var existingGasto = await _gastoService.GetByIdAsync(id);
        if (existingGasto == null)
            return NotFound();

        await _gastoService.DeleteAsync(id);
        return NoContent();
    }

    // GET: api/gastos/resumen?mes=6&anio=2025
    [HttpGet("resumen")]
    public async Task<ActionResult<GastosResumenDTO>> ObtenerResumenDelMes(int? mes, int? anio)
    {
        var resumen = await _gastoService.ObtenerResumenDelMes(mes, anio);
        return Ok(resumen);
    }
}

