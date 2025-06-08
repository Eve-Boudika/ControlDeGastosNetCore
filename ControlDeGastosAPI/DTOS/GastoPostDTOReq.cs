namespace ControlDeGastosAPI.DTOS
{
    public class GastoPostDTOReq
    {
        public int Monto { get; set; }

        public DateTime Fecha { get; set; }

        public string? Detalle { get; set; }

        public int CategoriaId { get; set; }
    }
}
