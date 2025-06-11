using ControlDeGastosAPI.DTOS;
using ControlDeGastosAPI.Models;
using ControlDeGastosAPI.Repositories;

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
        public async Task<GastoPostDTORes> CreateAsync(Gasto gasto)
        {
            GastoPostDTORes gastoPostDTORes = new GastoPostDTORes();
            gastoPostDTORes.Id = await _gastoRepository.AddAsync(gasto);
            return gastoPostDTORes;
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

        public async Task<GastosResumenDTO> ObtenerResumenDelMes(int? mes, int? anio)
        {
            var now = DateTime.Now;
            int mesValue = mes ?? now.Month;
            int anioValue = anio ?? now.Year;

            var gastos = await _gastoRepository.GetByMonthAndYearAsync(mesValue, anioValue);
            var presupuesto = await _presupuestoRepository.ObtenerPorMesYAnioAsync(mesValue, anioValue);

            var gastoDtos = gastos.Select(g => new GastoDTO
            {
                Id = g.Id,
                Detalle = g.Detalle,
                Monto = g.Monto,
                Fecha = g.Fecha,
                CategoriaId = g.CategoriaId,
                CategoriaNombre = g.Categoria?.Nombre ?? ""
            }).ToList();

            var categoriaDtos = gastos
                .Select(g => g.Categoria)
                .Where(c => c != null)
                .Distinct()
                .Select(c => new CategoriaDTO
                {
                    Id = c!.Id,
                    Nombre = c.Nombre
                })
                .ToList();

            return new GastosResumenDTO
            {
                Gastos = gastoDtos,
                TotalGastado = gastoDtos.Sum(g => g.Monto),
                MontoPresupuesto = presupuesto?.Monto,
                Periodo = new DateTime(anioValue, mesValue, 1),
                CategoriaSeleccionada = "",
                CategoriaSeleccionadaId = null,
                Categorias = categoriaDtos
            };
        }

        public async Task<GastoUpdateDTO>UpdateAsync(Gasto gasto)
        {
            await _gastoRepository.UpdateAsync(gasto);
            return new GastoUpdateDTO
            {
                Id = gasto.Id,
                Fecha = gasto.Fecha,
                Monto = gasto.Monto,
                CategoriaId = gasto.CategoriaId,
                Detalle = gasto.Detalle
            };
        }
    }
}
