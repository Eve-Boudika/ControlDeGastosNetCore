using System;
using System.ComponentModel.DataAnnotations;

namespace ControlDeGastosMVC.ViewModels
{
    public class PresupuestoViewModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Año { get; set; }

        [Required]
        public DateTime Mes { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un monto válido.")]
        public int Monto { get; set; }
    }
}
