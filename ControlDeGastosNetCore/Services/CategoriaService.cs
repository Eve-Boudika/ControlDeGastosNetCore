using System.Collections.Generic;
using ControlDeGastosNetCore.Models;
using ControlDeGastosNetCore.Repositories;

namespace ControlDeGastosNetCore.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IEnumerable<Categoria> GetAll()
        {
            return _categoriaRepository.GetAll();
        }

        public Categoria GetById(int id)
        {
            return _categoriaRepository.GetById(id);
        }

        public void Create(Categoria categoria)
        {
            _categoriaRepository.Add(categoria);
            _categoriaRepository.Save();
        }

        public void Update(Categoria categoria)
        {
            _categoriaRepository.Update(categoria);
            _categoriaRepository.Save();
        }

        public void Delete(int id)
        {
            var categoria = _categoriaRepository.GetById(id);
            if (categoria != null)
            {
                _categoriaRepository.Delete(categoria);
                _categoriaRepository.Save();
            }
        }
    }
}
