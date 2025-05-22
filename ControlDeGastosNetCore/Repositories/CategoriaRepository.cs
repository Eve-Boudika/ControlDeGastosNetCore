using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ControlDeGastosNetCore.Models;
using ControlDeGastosNetCore.Repositories;

namespace ControlDeGastosNetCore.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> GetAll()
        {
            return _context.Categorias.ToList();
        }

        public Categoria GetById(int id)
        {
            return _context.Categorias.Find(id);
        }

        public void Add(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
        }

        public void Update(Categoria categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;
        }

        public void Delete(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
