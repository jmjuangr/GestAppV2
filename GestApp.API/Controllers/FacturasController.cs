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
            try
            {
                var factura = _service.GenerarFacturaDesdePedido(dto.IdPedido);
                return Ok(new
                {
                    mensaje = $"Factura {factura.IdFactura} creada correctamente.",
                    total = factura.ImporteTotal
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Factura>> Get(
            [FromQuery] int? idPedido,
            [FromQuery] decimal? importeMin,
            [FromQuery] decimal? importeMax,
            [FromQuery] bool? estaPagada,
            [FromQuery] string? ordenarPor = "importe",
            [FromQuery] bool ascendente = true)
        {
            var facturas = _service.FiltrarFacturas(idPedido, importeMin, importeMax, estaPagada, ordenarPor, ascendente);
            return Ok(facturas);
        }

        [HttpPut("{id}/pagar")]
        [Authorize(Roles = "Admin")]
        public IActionResult MarcarComoPagada(int id)
        {
            var resultado = _service.MarcarComoPagada(id);
            if (!resultado)
                return NotFound($"Factura con ID {id} no encontrada.");

            return Ok($"Factura {id} marcada como pagada.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult EliminarFactura(int id)
        {
            var eliminada = _service.EliminarFactura(id);
            if (!eliminada)
                return NotFound($"Factura con ID {id} no encontrada.");

            return NoContent();
        }
    }
}
