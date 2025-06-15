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
            var productos = _service.ObtenerProductosPorIds(dto.ProductosIds);

            var pedido = new Pedido
            {
                Fecha = DateTime.Now,
                Confirmado = false,
                Productos = productos
            };

            _service.GuardarPedido(pedido);

            return Ok(pedido);
        }

        [HttpGet]
        public ActionResult<List<Pedido>> Get(
            [FromQuery] DateTime? fechaMin,
            [FromQuery] DateTime? fechaMax,
            [FromQuery] bool? confirmado,
            [FromQuery] string? ordenarPor = "fecha",
            [FromQuery] bool ascendente = true)
        {
            var pedidos = _service.FiltrarPedidos(fechaMin, fechaMax, confirmado, ordenarPor, ascendente);
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public ActionResult<Pedido> GetById(int id)
        {
            var pedido = _service.ObtenerPorId(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarPedido(int id, [FromBody] PedidoCreateDTO dto)
        {
            var pedidoExistente = _service.ObtenerPorId(id);
            if (pedidoExistente == null) return NotFound();

            if (dto.ProductosIds?.Any() == true)
                pedidoExistente.Productos = _service.ObtenerProductosPorIds(dto.ProductosIds);

            pedidoExistente.Confirmado = true;

            _service.GuardarPedido(pedidoExistente);

            return Ok($"Pedido {id} actualizado y confirmado.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult EliminarPedido(int id)
        {
            var eliminado = _service.EliminarPedido(id);
            if (!eliminado)
                return NotFound($"Pedido con ID {id} no encontrado.");

            return Ok($"Pedido {id} eliminado.");
        }
    }
}
