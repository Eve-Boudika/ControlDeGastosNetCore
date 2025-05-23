using System.Linq;
using ControlDeGastosNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlDeGastosNetCore.Repository
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        private readonly AppDbContext _context;

        public PresupuestoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Presupuesto> ObtenerTodos()
        {
            return _context.Presupuestos.ToList();
        }

        public Presupuesto ObtenerPorId(int id)
        {
            return _context.Presupuestos.Find(id);
        }

        public bool ExistePresupuestoParaMesAnio(int mes, int anio)
        {
            return _context.Presupuestos.Any(p => p.Mes.Month == mes && p.Año.Year == anio);
        }

        public Presupuesto? ObtenerPorMesYAnio(int mes, int anio)
        {
            return _context.Presupuestos
                .FirstOrDefault(p => p.Mes.Month == mes && p.Año.Year == anio);
        }

        public void Crear(Presupuesto presupuesto)
        {
            _context.Presupuestos.Add(presupuesto);
        }

        public void Editar(Presupuesto presupuesto)
        {
            _context.Entry(presupuesto).State = EntityState.Modified;
        }

        public void Eliminar(int id)
        {
            var presupuesto = _context.Presupuestos.Find(id);
            if (presupuesto != null)
            {
                _context.Presupuestos.Remove(presupuesto);
            }
        }

        public async Task<Presupuesto?> ObtenerPorMesYAnioAsync(int mes, int anio)
        {
            return await _context.Presupuestos
                .FirstOrDefaultAsync(p => p.Mes.Month == mes && p.Año.Year == anio);
        }
    }
}
