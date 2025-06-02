using ControlDeGastosAPI.Models;

namespace ControlDeGastosAPI.ViewModels
{
    public class GastosResumenViewModel
    {
        public List<Gasto> Gastos { get; set; } = new();
        public int TotalGastado { get; set; }
        public int? MontoPresupuesto { get; set; }
        public DateTime Periodo { get; set; }
        public string CategoriaSeleccionada { get; set; } = "";
        public int? CategoriaSeleccionadaId { get; set; }
        public List<Categoria> Categorias { get; set; } = new();
    }
}
