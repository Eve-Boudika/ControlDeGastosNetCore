using ControlDeGastosAPI.Models;

namespace ControlDeGastosAPI.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> ObtenerTodasAsync();
        Task<Categoria?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Categoria categoria);
        Task ActualizarAsync(Categoria categoria);
        Task EliminarAsync(int id);
    }
}
