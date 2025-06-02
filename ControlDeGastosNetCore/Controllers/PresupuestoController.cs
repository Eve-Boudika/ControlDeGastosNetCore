using ControlDeGastosMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace ControlDeGastosMVC.Controllers
{
    public class PresupuestoController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:5001/api/presupuesto"; // Cambiá esto si tu API corre en otro puerto

        public PresupuestoController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Presupuesto
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var presupuestos = JsonSerializer.Deserialize<List<PresupuestoViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(presupuestos);
            }

            return View(new List<PresupuestoViewModel>());
        }

        // GET: Presupuesto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Presupuesto/Create
        [HttpPost]
        public async Task<IActionResult> Create(PresupuestoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiBaseUrl, content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al crear el presupuesto");
            return View(model);
        }

        // GET: Presupuesto/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var presupuesto = JsonSerializer.Deserialize<PresupuestoViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(presupuesto);
        }

        // POST: Presupuesto/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(PresupuestoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{model.Id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al editar el presupuesto");
            return View(model);
        }

        // GET: Presupuesto/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var presupuesto = JsonSerializer.Deserialize<PresupuestoViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(presupuesto);
        }

        // POST: Presupuesto/DeleteConfirmed
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al eliminar el presupuesto");
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
