namespace ControlDeGastosAPI.DTOS
{
    public class PresupuestoResumenDTO
    {
        public int PresupuestoTotal { get; set; }
        public int GastosTotales { get; set; }
        public int PresupuestoDisponible => PresupuestoTotal - GastosTotales;
    }
}
