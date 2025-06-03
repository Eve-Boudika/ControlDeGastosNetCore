using ControlDeGastosAPI.DTOs;
using ControlDeGastosAPI.Models;

namespace ControlDeGastosAPI.Services
{
    public interface IGastoService
    {
        Task<List<Gasto>> GetAllAsync();
        Task<Gasto?> GetByIdAsync(int id);
        Task CreateAsync(Gasto gasto);
        Task UpdateAsync(Gasto gasto);
        Task DeleteAsync(int id);
        Task<GastosResumenDTO> ObtenerResumenDelMes(int? mes, int? anio);
    }
}
