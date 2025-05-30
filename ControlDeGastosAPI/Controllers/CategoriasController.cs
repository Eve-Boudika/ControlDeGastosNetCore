using Microsoft.AspNetCore.Mvc;
using ControlDeGastosAPI.Models;
using ControlDeGastosAPI.Services;

namespace ControlDeGastosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categorias = await _categoriaService.ObtenerTodasAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var categoria = await _categoriaService.ObtenerPorIdAsync(id);
            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Categoria categoria)
        {
            await _categoriaService.CrearAsync(categoria);
            return CreatedAtAction(nameof(Get), new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.Id)
                return BadRequest();

            await _categoriaService.ActualizarAsync(categoria);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoriaService.EliminarAsync(id);
            return NoContent();
        }
    }
}