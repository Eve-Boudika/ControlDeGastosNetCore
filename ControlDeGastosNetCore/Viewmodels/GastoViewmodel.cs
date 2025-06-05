using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ControlDeGastosNetCore.ViewModels
{
public class GastoViewmodel
{
    public int Id { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar un monto válido.")]
    public int Monto { get; set; }

    [Required(ErrorMessage = "Debe seleccionar una categoría.")]
    public int CategoriaId { get; set; }

    public string? Detalle { get; set; }

    // Nombre legible (por si lo necesitás para mostrar en otras vistas)
    public string? CategoriaNombre { get; set; }

    // Lista para el DropDownList
    public List<SelectListItem>? Categorias { get; set; }

}

}
