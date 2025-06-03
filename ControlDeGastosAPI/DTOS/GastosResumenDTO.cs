namespace ControlDeGastosAPI.DTOs
{
    public class GastosResumenDTO
    {
        public List<GastoDTO> Gastos { get; set; } = new();
        public int TotalGastado { get; set; }
        public int? MontoPresupuesto { get; set; }
        public DateTime Periodo { get; set; }
        public string CategoriaSeleccionada { get; set; } = "";
        public int? CategoriaSeleccionadaId { get; set; }
        public List<CategoriaDTO> Categorias { get; set; } = new();
    }
}

