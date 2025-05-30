using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ControlDeGastosAPI.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        
    }
}
