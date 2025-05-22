using ControlDeGastosNetCore.Models;

namespace ControlDeGastosNetCore.Repositories
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> GetAll();
        Categoria GetById(int id);
        void Add(Categoria categoria);
        void Update(Categoria categoria);
        void Delete(Categoria categoria);
        void Save();
    }
}
