using CamiFarma_I.Models;
using CamiFarma_I.Services;
using Microsoft.AspNetCore.Mvc;

namespace CamiFarma_I.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoService _productoService;

        public ProductoController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        // GET: /Productos
        public IActionResult Index()
        {
            var productos = _productoService.ObtenerTodos();
            return View(productos);
        }

        // GET: /Producto/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /Producto/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _productoService.Insertar(producto);
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: /Producto/Editar/5
        public IActionResult Editar(int id)
        {
            var producto = _productoService.ObtenerPorId(id); 
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: /Producto/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _productoService.Editar(producto);
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: /Producto/Eliminar/5
        public IActionResult Eliminar(int id)
        {
            var producto = _productoService.ObtenerPorId(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: /Producto/EliminarConfirmado/5
        [HttpPost, ActionName("EliminarConfirmado")]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarConfirmado(int id)
        {
            _productoService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
