using ControlDeGastosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlDeGastosAPI.Repositories
{
    public class PresupuestoRepository : IPresupuestoRepository
    {

        private readonly AppDbContext _context;
        public PresupuestoRepository(AppDbContext context)
        {
            _context = context;
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
            var presupuesto = _context.Presupuestos.Find(id);
            if (presupuesto != null)
            {
                _context.Presupuestos.Remove(presupuesto);
                _context.SaveChanges();
            }
        }

        public bool ExistePresupuestoParaMesAnio(int mes, int anio)
        {
            throw new NotImplementedException();
        }

        public Presupuesto ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Presupuesto?> ObtenerPorMesYAnioAsync(int mes, int anio)
        {
            return await _context.Presupuestos
                .FirstOrDefaultAsync(p => p.Mes.Month == mes && p.Año.Year == anio);
        }

        public List<Presupuesto> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
