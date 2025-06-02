using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using ControlDeGastosMVC.ViewModels;

namespace ControlDeGastosMVC.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:5001/api/categoria";

        public CategoriaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            var json = await response.Content.ReadAsStringAsync();
            var categorias = JsonSerializer.Deserialize<List<CategoriaViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(categorias);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoriaViewModel model)
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
            var categoria = JsonSerializer.Deserialize<CategoriaViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoriaViewModel model)
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
            var categoria = JsonSerializer.Deserialize<CategoriaViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}

