using ControlDeGastosAPI.Models;
using ControlDeGastosAPI.ViewModels;

namespace ControlDeGastosAPI.Services
{
    public interface IGastoService
    {
        Task<List<Gasto>> GetAllAsync();
        Task<Gasto?> GetByIdAsync(int id);
        Task CreateAsync(Gasto gasto);
        Task UpdateAsync(Gasto gasto);
        Task DeleteAsync(int id);
        Task<GastosResumenViewModel> ObtenerResumenDelMes(int? mes, int? anio);
    }
}
