using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ControlDeGastosNetCore.Models;

namespace ControlDeGastosNetCore.Repository
{
    public class GastoRepository : IGastoRepository
    {
        private readonly AppDbContext _context;

        public async Task<List<Gasto>> ObtenerPorMesYAnioAsync(int mes, int anio)
        {
            return await _context.Gastos
                .Include(g => g.Categoria)
                .Where(g => g.Fecha.Month == mes && g.Fecha.Year == anio)
                .ToListAsync();
        }

        public GastoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gasto> GetAll()
        {
            return _context.Gastos.Include(g => g.Categoria).ToList(); // si hay relación con Categoría
        }

        public IEnumerable<Gasto> ObtenerPorMesYAnio(int mes, int anio)
        {
            return _context.Gastos
                .Include(g => g.Categoria)
                .Where(g => g.Fecha.Month == mes && g.Fecha.Year == anio)
                .ToList();
        }

        public Gasto GetById(int id)
        {
            return _context.Gastos.Find(id);
        }

        public void Add(Gasto gasto)
        {
            _context.Gastos.Add(gasto);
        }

        public void Update(Gasto gasto)
        {
            _context.Entry(gasto).State = EntityState.Modified;
        }

        public void Delete(Gasto gasto)
        {
            _context.Gastos.Remove(gasto);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
