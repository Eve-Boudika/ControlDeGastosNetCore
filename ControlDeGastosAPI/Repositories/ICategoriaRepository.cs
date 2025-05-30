using ControlDeGastosAPI.Models;

namespace ControlDeGastosAPI.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAllAsync();
        Task<Categoria?> GetByIdAsync(int id);
        Task AddAsync(Categoria categoria);
        void Update(Categoria categoria);
        void Delete(Categoria categoria);
        Task SaveAsync();

    }
}
