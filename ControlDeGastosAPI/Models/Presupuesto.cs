using System;
using System.Collections.Generic;

namespace ControlDeGastosAPI.Models;

public partial class Presupuesto
{
    public int Id { get; set; }

    public DateTime Año { get; set; }

    public DateTime Mes { get; set; }

    public int Monto { get; set; }
}
