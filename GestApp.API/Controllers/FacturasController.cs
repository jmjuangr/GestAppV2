using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GestApp.Models;
using GestApp.Business.Services;

namespace GestApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User,Admin")]
    public class FacturasController : ControllerBase
    {
        private readonly FacturaService _service;

        public FacturasController(FacturaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CrearFactura([FromBody] FacturaCreateDTO dto)
        {
            var factura = new Factura(dto.IdFactura, dto.IdPedido, dto.Productos);
            _service.GuardarFactura(factura);

            return Ok($"Factura {factura.IdFactura} creada correctamente. Total: {factura.ImporteTotal} €");
        }

        [HttpGet]
        public ActionResult<List<Factura>> Get(
     [FromQuery] int? idPedido,
     [FromQuery] decimal? importeMin,
     [FromQuery] decimal? importeMax,
     [FromQuery] bool? estaPagada,
     [FromQuery] string? ordenarPor = "importe",
     [FromQuery] bool ascendente = true
 )
        {
            var facturas = _service.FiltrarFacturas(idPedido, importeMin, importeMax, estaPagada, ordenarPor, ascendente);
            return Ok(facturas);
        }

    }
}
