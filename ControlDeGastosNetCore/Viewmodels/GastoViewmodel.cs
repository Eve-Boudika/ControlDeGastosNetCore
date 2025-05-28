using ControlDeGastosNetCore.Models;
using System.ComponentModel.DataAnnotations;

namespace ControlDeGastosNetCore.Viewmodels
{
    public class GastoViewmodel
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public int Monto { get; set; }
        

        [Required]
        public DateTime Fecha { get; set; }

        public string? Detalle { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        public IEnumerable<Categoria>? Categorias { get; set; }
    }
}
