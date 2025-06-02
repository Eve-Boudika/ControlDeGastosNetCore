using ControlDeGastosAPI.Models;
using ControlDeGastosAPI.Repositories;
using ControlDeGastosAPI.ViewModels;

namespace ControlDeGastosAPI.Services
{
    public class GastoService : IGastoService
    {
        private readonly IGastoRepository _gastoRepository;
        private readonly IPresupuestoRepository _presupuestoRepository;

        public GastoService(IGastoRepository gastoRepository, IPresupuestoRepository presupuestoRepository)
        {
            _gastoRepository = gastoRepository;
            _presupuestoRepository = presupuestoRepository;
        }
        public async Task CreateAsync(Gasto gasto)
        {
            await _gastoRepository.AddAsync(gasto);
        }

        public async Task DeleteAsync(int id)
        {
            await _gastoRepository.DeleteAsync(id);
        }

        public async Task<List<Gasto>> GetAllAsync()
        {
            return await _gastoRepository.GetAllAsync();
        }

        public async Task<Gasto?> GetByIdAsync(int id)
        {
            return await _gastoRepository.GetByIdAsync(id);
        }

        public async Task<GastosResumenViewModel> ObtenerResumenDelMes(int? mes, int? anio)
        {
            var now = DateTime.Now;
            int mesValue = mes ?? now.Month;
            int anioValue = anio ?? now.Year;

            var gastos = await _gastoRepository.GetByMonthAndYearAsync(mesValue, anioValue);
            var presupuesto = await _presupuestoRepository.ObtenerPorMesYAnioAsync(mesValue, anioValue);

            return new GastosResumenViewModel
            {
                Gastos = gastos,
                TotalGastado = gastos.Sum(g => g.Monto),
                MontoPresupuesto = presupuesto?.Monto,
                Periodo = new DateTime(anioValue, mesValue, 1),
                CategoriaSeleccionada = "",
                CategoriaSeleccionadaId = null,
                Categorias = gastos.Select(g => g.Categoria).Distinct().ToList()
            };
        }

        public async Task UpdateAsync(Gasto gasto)
        {
            await _gastoRepository.UpdateAsync(gasto);
        }
    }
}
