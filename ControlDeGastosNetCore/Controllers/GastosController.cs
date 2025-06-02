using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using ControlDeGastosNetCore.ViewModels;

namespace ControlDeGastosNetCore.Controllers
{
    public class GastoController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:5001/api/gasto";

        public GastoController(HttpClient httpClient)
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


