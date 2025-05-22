using System.Collections.Generic;
using ControlDeGastosNetCore.Models;
using ControlDeGastosNetCore.Viewmodels;


namespace ControlDeGastosNetCore.Services
{
    public interface IGastoService
    {
        IEnumerable<Gasto> GetAll();
        Gasto GetById(int id);
        void Create(Gasto gasto);
        void Update(Gasto gasto);
        void Delete(int id);

        Task<GastosResumenViewmodel> ObtenerResumenDelMes(int? mes, int? anio);
        Task CrearGastoAsync(Gasto gasto);
    }


}

