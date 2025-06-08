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

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            var json = await response.Content.ReadAsStringAsync();
            var gastos = JsonSerializer.Deserialize<List<GastoViewmodel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(gastos);
        }


        public async Task<IActionResult> resumen(int? mes, int? anio)
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
                return RedirectToAction(nameof(Index));
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

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
            var json = await response.Content.ReadAsStringAsync();
            var gasto = JsonSerializer.Deserialize<GastoViewmodel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(gasto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GastoViewmodel model)
        {
            if (!ModelState.IsValid) return View(model);

            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{model.Id}", content);
            return response.IsSuccessStatusCode ? RedirectToAction(nameof(Index)) : View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
            var json = await response.Content.ReadAsStringAsync();
            var gasto = JsonSerializer.Deserialize<GastoViewmodel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(gasto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
            return RedirectToAction(nameof(Index));
        }


    }
}


