using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using ControlDeGastosMVC.ViewModels;

namespace ControlDeGastosMVC.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5270/api/categorias";

        public CategoriasController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            var json = await response.Content.ReadAsStringAsync();
            var categorias = JsonConvert.DeserializeObject<List<CategoriaViewModel>>(json);
            return View(categorias);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoriaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiBaseUrl, content);
            return response.IsSuccessStatusCode ? RedirectToAction(nameof(Index)) : View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
            var json = await response.Content.ReadAsStringAsync();
            var categoria = JsonConvert.DeserializeObject<CategoriaViewModel>(json);
            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoriaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{model.Id}", content);
            return response.IsSuccessStatusCode ? RedirectToAction(nameof(Index)) : View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "No se pudo obtener la categoría.";
                return View("Error");
            }

            var json = await response.Content.ReadAsStringAsync();
            var categoria = JsonConvert.DeserializeObject<CategoriaViewModel>(json);
            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Error = "No se pudo eliminar la categoría.";
            return View("Error");
        }
    }
}

