namespace ControlDeGastosAPI.DTOS
{
    public class GastoUpdateDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Monto { get; set; }
        public int CategoriaId { get; set; }
        public string? Detalle { get; set; }
    }
}
