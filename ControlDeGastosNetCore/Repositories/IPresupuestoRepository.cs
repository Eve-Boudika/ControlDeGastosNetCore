using ControlDeGastosNetCore.Models;

public interface IPresupuestoRepository
{
    List<Presupuesto> ObtenerTodos();
    Presupuesto ObtenerPorId(int id);
    bool ExistePresupuestoParaMesAnio(int mes, int anio);
    void Crear(Presupuesto presupuesto);
    void Editar(Presupuesto presupuesto);
    void Eliminar(int id);
    Task<Presupuesto?> ObtenerPorMesYAnioAsync(int mes, int anio);
}
