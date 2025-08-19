using CamiFarma_I.Models;
using CamiFarma_I.Services;
using Microsoft.AspNetCore.Mvc;

namespace CamiFarma_I.Controllers
{
    public class SalidaController : Controller
    {
        private readonly SalidaService _salidaService;
        private readonly ProductoService _productoService;

        public SalidaController(SalidaService salidaService, ProductoService productoService)
        {
            _salidaService = salidaService;
            _productoService = productoService;
        }

        // GET: /Salida/Registrar
        public IActionResult Registrar()
        {
            ViewBag.Productos = _productoService.ObtenerTodos();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(int productoId, int cantidad)
        {
            try
            {
                _salidaService.RegistrarSalida(productoId, cantidad);
                TempData["Mensaje"] = "Salida registrada correctamente.";
                return RedirectToAction("ReporteDiario");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return RedirectToAction("Registrar");
            }
        }

        // GET: /Salida/ReporteDiario
        public IActionResult ReporteDiario()
        {
            var salidas = _salidaService.ListarSalidasDiarias();
            return View(salidas);
        }

        // GET: /Salida/ReportePorRango
        public IActionResult ReportePorRango(DateTime? inicio, DateTime? fin)
        {
            if (inicio.HasValue && fin.HasValue)
            {
                var salidas = _salidaService.ListarSalidasPorRango(inicio.Value, fin.Value);
                return View(salidas);
            }
            return View(new List<SalidaReporte>());
        }
    }
}
