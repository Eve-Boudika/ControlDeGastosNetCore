using ControlDeGastosNetCore.Models;
using ControlDeGastosNetCore.Services;
using Microsoft.EntityFrameworkCore;

namespace ControlDeGastosNetCore.Services
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly IPresupuestoRepository _repository;

        public PresupuestoService(IPresupuestoRepository repository)
        {
            _repository = repository;
        }

        public List<Presupuesto> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public Presupuesto ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public bool ExistePresupuestoParaMesAnio(int mes, int anio)
        {
            return _repository.ExistePresupuestoParaMesAnio(mes, anio);
        }

        public void Crear(Presupuesto presupuesto)
        {
            // Acá podrías agregar validaciones o reglas antes de crear
            _repository.Crear(presupuesto);
        }

        public void Editar(Presupuesto presupuesto)
        {
            // Validaciones también podrían ir acá
            _repository.Editar(presupuesto);
        }

        public void Eliminar(int id)
        {
            _repository.Eliminar(id);
        }
    }

}

