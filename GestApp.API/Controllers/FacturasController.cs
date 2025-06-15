
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
                // Genera la factura desde el pedido 
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
            [FromQuery] int? idPedido,              // filtra por ID de pedido 
            [FromQuery] decimal? importeMin,        // filtra por importe mínimo 
            [FromQuery] decimal? importeMax,        // filtra por importe máximo 
            [FromQuery] bool? estaPagada,           // filtra por si está pagada o no
            [FromQuery] string? ordenarPor = "importe", // campo por el que ordenar
            [FromQuery] bool ascendente = true)         // orden ascendente o descendente
        {
            //  obtener la lista filtrada y ordenada
            var facturas = _service.FiltrarFacturas(idPedido, importeMin, importeMax, estaPagada, ordenarPor, ascendente);


            return Ok(facturas);
        }


        [HttpPut("{id}/pagar")]
        [Authorize(Roles = "Admin")]
        public IActionResult MarcarComoPagada(int id)
        {
            // Marca la factura como pagada 
            var resultado = _service.MarcarComoPagada(id);


            if (!resultado)
                return NotFound($"Factura con ID {id} no encontrada.");


            return Ok($"Factura {id} marcada como pagada.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult EliminarFactura(int id)
        {
            // eliminar la factura 
            var eliminada = _service.EliminarFactura(id);

            // Si no se encuentra la factura, devuelve error 404
            if (!eliminada)
                return NotFound($"Factura con ID {id} no encontrada.");

            // Si se elimina correctamente, devuelve estado 204 (sin contenido)
            return NoContent();
        }
    }
}
