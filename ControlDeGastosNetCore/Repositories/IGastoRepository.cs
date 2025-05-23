using System.Collections.Generic;
using ControlDeGastosNetCore.Models;

namespace ControlDeGastosNetCore.Repository
{
    public interface IGastoRepository
    {
        IEnumerable<Gasto> GetAll();
        Gasto GetById(int id);
        void Add(Gasto gasto);
        void Update(Gasto gasto);
        void Delete(Gasto gasto);
        void Save();
        Task<List<Gasto>> ObtenerPorMesYAnioAsync(int mes, int anio);

    }
}


