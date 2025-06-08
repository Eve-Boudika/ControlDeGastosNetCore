using System;
using System.Collections.Generic;

namespace ControlDeGastosAPI.Models;

public partial class Categoria
{
    public int Id { get; set; }

    public string? Nombre { get; set; }
    public virtual ICollection<Gasto> Gastos { get; set; } = new List<Gasto>(); // Relación inversa

}
