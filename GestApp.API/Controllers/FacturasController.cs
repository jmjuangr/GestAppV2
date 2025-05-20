using Microsoft.AspNetCore.Mvc;
using GestApp.Models;
using GestApp.Business.Services;

namespace GestApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly FacturaService _service;

        public FacturasController()
        {
            _service = new FacturaService();
        }

        // POST /api/facturas
        [HttpPost]
        public IActionResult CrearFactura([FromBody] FacturaCreateDTO dto)
        {
            var factura = new Factura(dto.IdFactura, dto.IdPedido, dto.Productos);
            _service.GuardarFactura(factura);

            return Ok($"Factura {factura.IdFactura} creada correctamente. Total: {factura.ImporteTotal} €");
        }

        // GET /api/facturas
        [HttpGet]
        public ActionResult<List<Factura>> Get(
            [FromQuery] int? idPedido,
            [FromQuery] decimal? importeMin,
            [FromQuery] decimal? importeMax,
            [FromQuery] bool? estaPagada)
        {
            var facturas = _service.FiltrarFacturas(idPedido, importeMin, importeMax, estaPagada);
            return Ok(facturas);
        }
    }
}
