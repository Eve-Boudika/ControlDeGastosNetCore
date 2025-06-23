using ControlDeGastosAPI.Models;
using ControlDeGastosAPI.Repositories;
using ControlDeGastosAPI.DTOS;

namespace ControlDeGastosAPI.Services
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly IPresupuestoRepository _repository;
        private readonly IGastoRepository _gastoRepository;

        public PresupuestoService(IPresupuestoRepository repository, IGastoRepository gastoRepository)
        {
            _repository = repository;
            _gastoRepository = gastoRepository;
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

        public async Task<PresupuestoResumenDTO> ObtenerResumenDelMesAsync(int mes, int anio)
        {
            var presupuesto = await _repository.ObtenerPorMesYAnioAsync(mes, anio);
            var gastos = await _gastoRepository.ObtenerTotalGastosDelMesAsync(mes, anio);

            return new PresupuestoResumenDTO
            {
                PresupuestoTotal = presupuesto?.Monto ?? 0,
                GastosTotales = gastos
            };
        }
    }
}

