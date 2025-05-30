using ControlDeGastosAPI.Models;
using ControlDeGastosAPI.Repositories;

namespace ControlDeGastosAPI.Services
{
    public class PresupuestoService : IPresupuestoService
    {

        private readonly IPresupuestoRepository _repository;

        public PresupuestoService(IPresupuestoRepository repository)
        {
            _repository = repository;
        }
        public void Crear(Presupuesto presupuesto)
        {
            _repository.Crear(presupuesto);
        }

        public void Editar(Presupuesto presupuesto)
        {
            _repository.Editar(presupuesto);
        }

        public void Eliminar(int id)
        {
            _repository.Eliminar(id);
        }

        public bool ExistePresupuestoParaMesAnio(int mes, int anio)
        {
            return _repository.ExistePresupuestoParaMesAnio(mes, anio);
        }

        public Presupuesto ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public List<Presupuesto> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }
    }
}
