using System.Collections.Generic;
using ControlDeGastosNetCore.Models;

namespace ControlDeGastosNetCore.Services
{
    public interface ICategoriaService
    {
        IEnumerable<Categoria> GetAll();
        Categoria GetById(int id);
        void Create(Categoria categoria);
        void Update(Categoria categoria);
        void Delete(int id);
    }
}

