using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using ControlDeGastosNetCore.ViewModels;
using ControlDeGastosNetCore.DTO;

namespace ControlDeGastosNetCore.Controllers
{
    public class GastosController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5270/api/gastos";

        public GastosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            var json = await response.Content.ReadAsStringAsync();
            var gastos = JsonSerializer.Deserialize<List<GastoViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(GastoViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiBaseUrl, content);
            return response.IsSuccessStatusCode ? RedirectToAction(nameof(Index)) : View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
            var json = await response.Content.ReadAsStringAsync();
            var gasto = JsonSerializer.Deserialize<GastoViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(gasto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GastoViewModel model)
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
            var gasto = JsonSerializer.Deserialize<GastoViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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


