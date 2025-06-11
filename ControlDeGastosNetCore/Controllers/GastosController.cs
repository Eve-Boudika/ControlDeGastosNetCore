using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using ControlDeGastosNetCore.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using ControlDeGastosNetCore.ViewModels;

namespace ControlDeGastosNetCore.Controllers
{
    public class GastosController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5270/api/Gastos";

        public GastosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("algo")]
        public async Task<IActionResult> Resumenssss()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            var json = await response.Content.ReadAsStringAsync();
            var gastos = JsonSerializer.Deserialize<List<GastosResumenDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(gastos);
        }

        [HttpGet]
        public async Task<IActionResult> Resumen(int? mes, int? anio)
        {
            var url = $"{_apiBaseUrl}/resumen?mes={mes}&anio={anio}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return View("error"); 

            var json = await response.Content.ReadAsStringAsync();
            var resumen = JsonSerializer.Deserialize<GastosResumenDTO>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(resumen);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categoriasResponse = await _httpClient.GetAsync("http://localhost:5270/api/Categorias");

            if (!categoriasResponse.IsSuccessStatusCode)
            {
                // Podés manejar errores aquí si querés
                return View(new GastoViewmodel());
            }

            var json = await categoriasResponse.Content.ReadAsStringAsync();
            var categorias = JsonSerializer.Deserialize<List<CategoriaDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var model = new GastoViewmodel
            {
                Fecha = DateTime.Today,
                Categorias = categorias.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                }).ToList()
            };

            return View(model);
        }

        // POST: Gastos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GastoViewmodel model)
        {
            if (ModelState.IsValid is false)
            {
                model.Categorias = await ObtenerCategoriasAsync();
                return View(model);
            }

            var gastoDTO = new GastoDTO
            {
                Monto = model.Monto,
                Fecha = model.Fecha,
                Detalle = model.Detalle,
                CategoriaId = model.CategoriaId
            };

            var content = new StringContent(JsonSerializer.Serialize(gastoDTO), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiBaseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                TempData["MensajeExito"] = "Gasto registrado correctamente";
                return RedirectToAction(nameof(Resumen));
            }
            else
            {
                model.Categorias = await ObtenerCategoriasAsync();
                ModelState.AddModelError(string.Empty, "Error al guardar el gasto");
                return View(model);
            }
        }

        private async Task<List<SelectListItem>> ObtenerCategoriasAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5270/api/categorias");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var categorias = JsonSerializer.Deserialize<List<CategoriaDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return categorias.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre
            }).ToList();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Obtener el gasto por ID
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                // Log, mensaje o redirección en caso de error
                return RedirectToAction(nameof(Resumen));
            }

            var json = await response.Content.ReadAsStringAsync();

            var gasto = JsonSerializer.Deserialize<GastoViewmodel>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (gasto == null)
            {
                // Si no se pudo deserializar correctamente
                return RedirectToAction(nameof(Resumen));
            }

            // Obtener las categorías desde la API 
            var categoriasResponse = await _httpClient.GetAsync("http://localhost:5270/api/categorias");

            if (categoriasResponse.IsSuccessStatusCode)
            {
                var categoriasJson = await categoriasResponse.Content.ReadAsStringAsync();

                var categorias = JsonSerializer.Deserialize<List<CategoriaDTO>>(categoriasJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                gasto.Categorias = categorias?
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nombre,
                        Selected = c.Id == gasto.CategoriaId // Marca como seleccionada
                    })
                    .ToList();
            }
            else
            {
                gasto.Categorias = new List<SelectListItem>(); // por si falla, evitar null
            }

            return View(gasto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GastoViewmodel gasto)
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, volvemos a cargar las categorías
                var categoriasResponse = await _httpClient.GetAsync("http://localhost:5270/api/categorias");

                if (categoriasResponse.IsSuccessStatusCode)
                {
                    var categoriasJson = await categoriasResponse.Content.ReadAsStringAsync();
                    var categorias = JsonSerializer.Deserialize<List<CategoriaDTO>>(categoriasJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    gasto.Categorias = categorias?
                        .Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.Nombre,
                            Selected = c.Id == gasto.CategoriaId
                        })
                        .ToList();
                }

                return View(gasto);
            }

            // Si el modelo es válido, enviamos el PUT a la API
            var jsonGasto = JsonSerializer.Serialize(gasto);
            var content = new StringContent(jsonGasto, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{gasto.Id}", content);

            if (!response.IsSuccessStatusCode)
            {
                // Si falla la actualización, podría volver a mostrar la vista con un error
                ModelState.AddModelError(string.Empty, "Error al actualizar el gasto.");
                return View(gasto);
            }

            return RedirectToAction(nameof(Resumen)); // o lo que uses como página de resumen
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5270/api/Gastos/{id}");

            
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index)); 
            }

            var json = await response.Content.ReadAsStringAsync();
            var gasto = JsonSerializer.Deserialize<GastoViewmodel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(gasto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Resumen));
            }

            return RedirectToAction(nameof(Resumen));
        }
    }
}


