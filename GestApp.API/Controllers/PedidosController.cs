using Microsoft.AspNetCore.Mvc;
using GestApp.Business.Services;
using GestApp.Models;

namespace GestApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoService _service;

        public PedidosController()
        {
            _service = new PedidoService();
        }

        [HttpPost]
        public IActionResult CrearPedido([FromBody] PedidoCreateDTO dto)
        {
            var pedido = new Pedido(dto.IdPedido);

            foreach (var producto in dto.Productos)
            {
                pedido.AgregarProducto(producto);
            }

            _service.GuardarPedido(pedido);

            return Ok($"Pedido {pedido.IdPedido} guardado correctamente con fecha {pedido.Fecha}.");
        }

        [HttpGet]
        public ActionResult<List<Pedido>> Get(
            [FromQuery] DateTime? fechaMin,
            [FromQuery] DateTime? fechaMax,
            [FromQuery] bool? confirmado)
        {
            var pedidos = _service.FiltrarPedidos(fechaMin, fechaMax, confirmado);
            return Ok(pedidos);
        }
    }
}
