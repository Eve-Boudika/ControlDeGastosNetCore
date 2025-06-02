using ControlDeGastosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlDeGastosAPI.Repositories
{
    public class GastoRepository : IGastoRepository
    {
        private readonly AppDbContext _context;

        public GastoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Gasto gasto)
        {
            await _context.Gastos.AddAsync(gasto);
            await _context.SaveChangesAsync();
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
            return await _context.Gastos.Include(g => g.Categoria).ToListAsync();
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
