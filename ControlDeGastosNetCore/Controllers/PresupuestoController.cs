using Microsoft.AspNetCore.Mvc;
using ControlDeGastosNetCore.Models;
using ControlDeGastosNetCore.Services;

namespace ControlDeGastosNetCore.Controllers
{
    public class PresupuestoController : Controller
    {
        private readonly IPresupuestoService _presupuestoService;

        public PresupuestoController(IPresupuestoService presupuestoService)
        {
            _presupuestoService = presupuestoService;
        }

        // GET: Presupuesto
        public IActionResult Index()
        {
            var presupuestos = _presupuestoService.ObtenerTodos();
            return View(presupuestos);
        }

        // GET: Presupuesto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Presupuesto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int Mes, int Año, int Monto)
        {
            if (ModelState.IsValid)
            {
                if (_presupuestoService.ExistePresupuestoParaMesAnio(Mes, Año))
                {
                    ModelState.AddModelError("", "Ya existe un presupuesto para ese mes y año.");
                    return View(new Presupuesto { Mes = new DateTime(Año, Mes, 1), Año = new DateTime(Año, 1, 1), Monto = Monto });
                }

                var presupuesto = new Presupuesto
                {
                    Mes = new DateTime(Año, Mes, 1),
                    Año = new DateTime(Año, 1, 1),
                    Monto = Monto
                };

                _presupuestoService.Crear(presupuesto);
                return RedirectToAction(nameof(Index));
            }

            return View(new Presupuesto { Mes = new DateTime(Año, Mes, 1), Año = new DateTime(Año, 1, 1), Monto = Monto });
        }

        // GET: Presupuesto/Edit/5
        public IActionResult Edit(int id)
        {
            var presupuesto = _presupuestoService.ObtenerPorId(id);
            if (presupuesto == null)
                return NotFound();

            return View(presupuesto);
        }

        // POST: Presupuesto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Presupuesto presupuesto)
        {
            if (ModelState.IsValid)
            {
                _presupuestoService.Editar(presupuesto);
                return RedirectToAction(nameof(Index));
            }

            return View(presupuesto);
        }

        // GET: Presupuesto/Delete/5
        public IActionResult Delete(int id)
        {
            var presupuesto = _presupuestoService.ObtenerPorId(id);
            if (presupuesto == null)
                return NotFound();

            return View(presupuesto);
        }

        // POST: Presupuesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _presupuestoService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}