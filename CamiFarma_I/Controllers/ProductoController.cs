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
    }
}
