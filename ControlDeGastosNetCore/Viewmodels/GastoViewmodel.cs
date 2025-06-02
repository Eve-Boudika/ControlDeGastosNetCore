using System;
using System.ComponentModel.DataAnnotations;

namespace ControlDeGastosNetCore.ViewModels
{
    public class GastoViewModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un monto válido.")]
        public int Monto { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        public int CategoriaId { get; set; }

        public string? Descripcion { get; set; }

        // Opcional: nombre de la categoría para mostrar
        public string? CategoriaNombre { get; set; }
    }
}