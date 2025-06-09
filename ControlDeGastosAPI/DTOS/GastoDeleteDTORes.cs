namespace ControlDeGastosAPI.DTOS
{
    public class GastoDeleteDTORes
    {
        public int Id { get; set; }
        public int Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string? Detalle { get; set; }
        public int CategoriaId { get; set; }
    }
}
