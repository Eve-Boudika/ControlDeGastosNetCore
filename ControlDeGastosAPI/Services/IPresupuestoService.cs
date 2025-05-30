using ControlDeGastosAPI.Models;

namespace ControlDeGastosAPI.Services
{
    public interface IPresupuestoService
    {
        List<Presupuesto> ObtenerTodos();
        Presupuesto ObtenerPorId(int id);
        bool ExistePresupuestoParaMesAnio(int mes, int anio);
        void Crear(Presupuesto presupuesto);
        void Editar(Presupuesto presupuesto);
        void Eliminar(int id);

    }
}
