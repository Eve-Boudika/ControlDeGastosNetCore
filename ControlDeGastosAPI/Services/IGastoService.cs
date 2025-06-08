using ControlDeGastosAPI.DTOS;
using ControlDeGastosAPI.Models;

namespace ControlDeGastosAPI.Services
{
    public interface IGastoService
    {
        Task<List<Gasto>> GetAllAsync();
        Task<Gasto?> GetByIdAsync(int id);
        Task<GastoPostDTORes> CreateAsync(Gasto gasto);
        Task UpdateAsync(Gasto gasto);
        Task DeleteAsync(int id);
        Task<GastosResumenDTO> ObtenerResumenDelMes(int? mes, int? anio);
    }
}
