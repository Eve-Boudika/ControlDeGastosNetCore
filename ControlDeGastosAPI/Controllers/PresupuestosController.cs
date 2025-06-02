using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlDeGastosAPI.Models;

namespace ControlDeGastosAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PresupuestosController : ControllerBase
{
    private readonly AppDbContext _context;

    public PresupuestosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/presupuestos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Presupuesto>>> GetPresupuestos()
    {
        return await _context.Presupuestos.ToListAsync();
    }

    // GET: api/presupuestos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Presupuesto>> GetPresupuesto(int id)
    {
        var presupuesto = await _context.Presupuestos.FindAsync(id);

        if (presupuesto == null)
        {
            return NotFound();
        }

        return presupuesto;
    }

    // POST: api/presupuestos
    [HttpPost]
    public async Task<ActionResult<Presupuesto>> CreatePresupuesto(Presupuesto presupuesto)
    {
        // Validar que no exista uno para el mismo mes y año
        bool existe = await _context.Presupuestos.AnyAsync(p =>
            p.Mes.Month == presupuesto.Mes.Month &&
            p.Mes.Year == presupuesto.Mes.Year);

        if (existe)
        {
            return BadRequest("Ya existe un presupuesto para ese mes y año.");
        }

        presupuesto.Año = new DateTime(presupuesto.Mes.Year, 1, 1); // Por compatibilidad
        _context.Presupuestos.Add(presupuesto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPresupuesto), new { id = presupuesto.Id }, presupuesto);
    }

    // PUT: api/presupuestos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePresupuesto(int id, Presupuesto presupuesto)
    {
        if (id != presupuesto.Id)
        {
            return BadRequest();
        }

        _context.Entry(presupuesto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PresupuestoExists(id))
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

    // DELETE: api/presupuestos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePresupuesto(int id)
    {
        var presupuesto = await _context.Presupuestos.FindAsync(id);
        if (presupuesto == null)
        {
            return NotFound();
        }

        _context.Presupuestos.Remove(presupuesto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PresupuestoExists(int id)
    {
        return _context.Presupuestos.Any(e => e.Id == id);
    }
}
