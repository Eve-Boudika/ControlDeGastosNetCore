using System.Collections.Generic;
using ControlDeGastosNetCore.Models;
using ControlDeGastosNetCore.Repository;
using ControlDeGastosNetCore.Viewmodels;

namespace ControlDeGastosNetCore.Services
{
    public class GastoService : IGastoService
    {
        private readonly IGastoRepository _repository;

        public GastoService(IGastoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Gasto> GetAll()
        {
            return _repository.GetAll();
        }

        public Gasto GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Create(Gasto gasto)
        {
            _repository.Add(gasto);
            _repository.Save();
        }

        public void Update(Gasto gasto)
        {
            _repository.Update(gasto);
            _repository.Save();
        }

        public void Delete(int id)
        {
            var gasto = _repository.GetById(id);
            if (gasto != null)
            {
                _repository.Delete(gasto);
                _repository.Save();
            }
        }

        public Task<GastosResumenViewmodel> ObtenerResumenDelMes(int? mes, int? anio)
        {
            throw new NotImplementedException();
        }

        public Task CrearGastoAsync(Gasto gasto)
        {
            throw new NotImplementedException();
        }

        public Task<Gasto> ObtenerGastoPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditarGastoAsync(Gasto gasto)
        {
            throw new NotImplementedException();
        }

        public Task EliminarGastoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GastosResumenViewmodel> FiltrarPorCategoria(int? categoriaId, int? mes, int? anio)
        {
            throw new NotImplementedException();
        }

        Task<GastosResumenViewmodel> IGastoService.ObtenerResumenDelMes(int? mes, int? anio)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Gasto> IGastoService.GetAll()
        {
            throw new NotImplementedException();
        }

        Gasto IGastoService.GetById(int id)
        {
            throw new NotImplementedException();
        }

        void IGastoService.Create(Gasto gasto)
        {
            throw new NotImplementedException();
        }

        void IGastoService.Update(Gasto gasto)
        {
            throw new NotImplementedException();
        }

        void IGastoService.Delete(int id)
        {
            throw new NotImplementedException();
        }


        Task IGastoService.CrearGastoAsync(Gasto gasto)
        {
            throw new NotImplementedException();
        }
    }
}
