using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlDeGastosAPI.Models;

namespace ControlDeGastosAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GastosController : ControllerBase
{
    private readonly AppDbContext _context;

    public GastosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/gastos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Gasto>>> GetGastos()
    {
        return await _context.Gastos.Include(g => g.Categoria).ToListAsync();
    }

    // GET: api/gastos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Gasto>> GetGasto(int id)
    {
        var gasto = await _context.Gastos.Include(g => g.Categoria)
                                         .FirstOrDefaultAsync(g => g.Id == id);

        if (gasto == null)
        {
            return NotFound();
        }

        return gasto;
    }

    // POST: api/gastos
    [HttpPost]
    public async Task<ActionResult<Gasto>> CreateGasto(Gasto gasto)
    {
        _context.Gastos.Add(gasto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGasto), new { id = gasto.Id }, gasto);
    }

    // PUT: api/gastos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGasto(int id, Gasto gasto)
    {
        if (id != gasto.Id)
        {
            return BadRequest();
        }

        _context.Entry(gasto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GastoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/gastos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGasto(int id)
    {
        var gasto = await _context.Gastos.FindAsync(id);
        if (gasto == null)
        {
            return NotFound();
        }

        _context.Gastos.Remove(gasto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/gastos/resumen?mes=6&anio=2025
    [HttpGet("resumen")]
    public async Task<ActionResult<object>> ObtenerResumenDelMes(int? mes, int? anio)
    {
        var fechaActual = DateTime.Now;
        int mesValor = mes ?? fechaActual.Month;
        int anioValor = anio ?? fechaActual.Year;

        var gastos = await _context.Gastos
            .Include(g => g.Categoria)
            .Where(g => g.Fecha.Month == mesValor && g.Fecha.Year == anioValor)
            .ToListAsync();

        var totalGastado = gastos.Sum(g => g.Monto);

        return Ok(new
        {
            TotalGastado = totalGastado,
            Periodo = new DateTime(anioValor, mesValor, 1),
            Gastos = gastos,
            Categorias = gastos.Select(g => g.Categoria).Distinct().ToList()
        });
    }

    private bool GastoExists(int id)
    {
        return _context.Gastos.Any(e => e.Id == id);
    }
}

