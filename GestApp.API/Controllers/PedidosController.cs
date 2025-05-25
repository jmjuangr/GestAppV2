using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GestApp.Business.Services;
using GestApp.Models;

namespace GestApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User,Admin")]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoService _service;

        public PedidosController(PedidoService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CrearPedido([FromBody] PedidoCreateDTO dto)
        {
            var pedido = new Pedido(dto.IdPedido)
            {
                Productos = dto.Productos
            };

            _service.GuardarPedido(pedido);

            return Ok($"Pedido {pedido.IdPedido} guardado correctamente con fecha {pedido.Fecha}.");
        }

        [HttpGet]
        public ActionResult<List<Pedido>> Get(
            [FromQuery] DateTime? fechaMin,
            [FromQuery] DateTime? fechaMax,
            [FromQuery] bool? confirmado,
            [FromQuery] string? ordenarPor = "fecha",
            [FromQuery] bool ascendente = true
 )
        {
            var pedidos = _service.FiltrarPedidos(fechaMin, fechaMax, confirmado, ordenarPor, ascendente);
            return Ok(pedidos);
        }

    }
}
