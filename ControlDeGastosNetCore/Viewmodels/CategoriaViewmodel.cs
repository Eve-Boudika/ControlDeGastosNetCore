using System.ComponentModel.DataAnnotations;

namespace ControlDeGastosMVC.ViewModels
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        public string? Descripcion { get; set; }
    }
}
