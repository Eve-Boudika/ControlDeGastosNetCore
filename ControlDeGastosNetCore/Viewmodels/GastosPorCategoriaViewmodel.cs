using ControlDeGastosNetCore.Models;

namespace ControlDeGastosNetCore.Viewmodels
{

    public class GastosPorCategoriaViewmodel
    {
        public DateTime Periodo { get; set; }

        public List<GastoCategoriaItem> GastosPorCategoria { get; set; }

        public class GastoCategoriaItem
        {
            public string NombreCategoria { get; set; }
            public int TotalGastado { get; set; }
            public double Porcentaje { get; set; } // Ej: 0.25 para 25%
        }
    }
}


