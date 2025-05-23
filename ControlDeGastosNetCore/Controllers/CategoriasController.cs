using Microsoft.AspNetCore.Mvc;
using System.Net;
using ControlDeGastosNetCore.Models;
using ControlDeGastosNetCore.Viewmodels;
using ControlDeGastosNetCore.Services;

namespace ControlDeGastosNetCore.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public IActionResult Index()
        {
            return View(_categoriaService.GetAll());
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return  BadRequest();

            var categoria = _categoriaService.GetById(id.Value);
            if (categoria == null) return BadRequest();

            return View(categoria);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriaViewmodel model)
        {
            if (ModelState.IsValid)
            {
                var categoria = new Categoria
                {
                    Id = model.Id,
                    Nombre = model.Nombre
                };

                _categoriaService.Create(categoria);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null) return  BadRequest();

            var categoria = _categoriaService.GetById(id.Value);
            if (categoria == null) return BadRequest();

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoriaViewmodel model)
        {
            if (ModelState.IsValid)
            {
                var categoria = new Categoria
                {
                    Id = model.Id,
                    Nombre = model.Nombre
                };

                _categoriaService.Update(categoria);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();

            var categoria = _categoriaService.GetById(id.Value);
            if (categoria == null) return BadRequest();

            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoriaService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

