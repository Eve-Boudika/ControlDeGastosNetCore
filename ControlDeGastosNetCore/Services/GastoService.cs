using ControlDeGastosNetCore.Models;
using ControlDeGastosNetCore.Repository;
using ControlDeGastosNetCore.Viewmodels;

namespace ControlDeGastosNetCore.Services
{
    public class GastoService : IGastoService
    {
        private readonly IGastoRepository _repository;

        private readonly IPresupuestoRepository _presupuestoRepository;

        public GastoService(IGastoRepository repository, IPresupuestoRepository presupuestoRepository)
        {
            _repository = repository;
            _presupuestoRepository = presupuestoRepository;
        }

        public Task CrearGastoAsync(Gasto gasto)
        {
            _repository.Add(gasto);
            _repository.Save();
            return Task.CompletedTask; 
        }

        public void Create(Gasto gasto)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Gasto> GetAll()
        {
            throw new NotImplementedException();
        }

        public Gasto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GastosResumenViewmodel> ObtenerResumenDelMes(int? mes, int? anio)
        {
            var fechaActual = DateTime.Now;
            int mesValor = mes ?? fechaActual.Month;
            int anioValor = anio ?? fechaActual.Year;

            var gastos = await _repository.ObtenerPorMesYAnioAsync(mesValor, anioValor);
            var presupuesto = await _presupuestoRepository.ObtenerPorMesYAnioAsync(mesValor, anioValor);

            var totalGastado = gastos.Sum(g => g.Monto);

            return new GastosResumenViewmodel
            {
                Gastos = gastos,
                TotalGastado = totalGastado,
                MontoPresupuesto = presupuesto?.Monto,
                Periodo = new DateTime(anioValor, mesValor, 1),
                CategoriaSeleccionada = "",
                CategoriaSeleccionadaId = null,
                Categorias = gastos.Select(g => g.Categoria).Distinct().ToList()
            };
        }

        public void Update(Gasto gasto)
        {
            throw new NotImplementedException();
        }
    }
}
