namespace ControlDeGastosAPI.DTOs
{
    public class GastoDTO
    {
        public int Id { get; set; }
        public string Detalle { get; set; } = "";
        public int Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; } = "";
    }
}
