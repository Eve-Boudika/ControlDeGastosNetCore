using ControlDeGastosAPI.Models;
using ControlDeGastosAPI.Repositories;

namespace ControlDeGastosAPI.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task CrearAsync(Categoria categoria)
        {
            await _categoriaRepository.AddAsync(categoria);
            await _categoriaRepository.SaveAsync();
        }

        public async Task ActualizarAsync(Categoria categoria)
        {
            _categoriaRepository.Update(categoria);
            await _categoriaRepository.SaveAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria != null)
            {
                _categoriaRepository.Delete(categoria);
                await _categoriaRepository.SaveAsync();
            }
        }

        public async Task<Categoria?> ObtenerPorIdAsync(int id)
        {
            return await _categoriaRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Categoria>> ObtenerTodasAsync()
        {
            return await _categoriaRepository.GetAllAsync();
        }
    }
}
