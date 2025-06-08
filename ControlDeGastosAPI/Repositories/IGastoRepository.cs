using ControlDeGastosAPI.Models;

namespace ControlDeGastosAPI.Repositories
{
    public interface IGastoRepository
    {
        Task<List<Gasto>> GetAllAsync();
        Task<Gasto?> GetByIdAsync(int id);
        Task<int> AddAsync(Gasto gasto);
        Task UpdateAsync(Gasto gasto);
        Task DeleteAsync(int id);
        Task<List<Gasto>> GetByMonthAndYearAsync(int mes, int anio);
    }
}
