using ControlDeGastosNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlDeGastosNetCore.Repositories
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
            return _context.Presupuestos
                .OrderByDescending(p => p.Año)
                .ThenByDescending(p => p.Mes)
                .ToList();
        }

        public Presupuesto ObtenerPorId(int id)
        {
            return _context.Presupuestos.Find(id);
        }

        public bool ExistePresupuestoParaMesAnio(int mes, int anio)
        {
            return _context.Presupuestos.Any(p => p.Mes.Month == mes && p.Año.Year == anio);
        }

        public void Crear(Presupuesto presupuesto)
        {
            _context.Presupuestos.Add(presupuesto);
            _context.SaveChanges();
        }

        public void Editar(Presupuesto presupuesto)
        {
            _context.Entry(presupuesto).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var presupuesto = ObtenerPorId(id);
            if (presupuesto != null)
            {
                _context.Presupuestos.Remove(presupuesto);
                _context.SaveChanges();
            }
        }
    }

}
