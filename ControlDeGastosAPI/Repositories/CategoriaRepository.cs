using ControlDeGastosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlDeGastosAPI.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
        }

        public void Delete(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria?> GetByIdAsync(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Categoria categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;
        }
    }
}
