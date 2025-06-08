using ControlDeGastosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ControlDeGastosAPI.Repositories
{
    public class GastoRepository : IGastoRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<GastoRepository> _logger;

        public GastoRepository(AppDbContext context, ILogger<GastoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<int> AddAsync(Gasto gasto)
        {
            try
            {
                var responeId = await _context.Gastos.AddAsync(gasto);//responde id insertado
                var responseInsert = await _context.SaveChangesAsync();//podrias manejar exepcion si response insert no retorna 1
                return responeId.Entity.Id; //retorna el id del gasto insertado
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Post creado con errores ", ex.InnerException.ToString());
                throw new ArgumentException("Error al crear entidad");
            }

        }

        public async Task DeleteAsync(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto != null)
            {
                _context.Gastos.Remove(gasto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Gasto>> GetAllAsync()
        {
            return await _context.Gastos.ToListAsync();
        }

        public async Task<Gasto?> GetByIdAsync(int id)
        {
            return await _context.Gastos.Include(g => g.Categoria)
                                        .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<Gasto>> GetByMonthAndYearAsync(int mes, int anio)
        {
            return await _context.Gastos.Include(g => g.Categoria)
                .Where(g => g.Fecha.Month == mes && g.Fecha.Year == anio)
                .ToListAsync();
        }

        public async Task UpdateAsync(Gasto gasto)
        {
            _context.Entry(gasto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
