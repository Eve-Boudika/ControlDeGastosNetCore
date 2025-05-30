using System;
using System.Collections.Generic;

namespace ControlDeGastosAPI.Models;

public partial class Gasto
{
    public int Id { get; set; }

    public int Monto { get; set; }

    public DateTime Fecha { get; set; }

    public string? Detalle { get; set; }

    public int CategoriaId { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;
}
