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

        // POST /api/pedidos
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

    }
}
