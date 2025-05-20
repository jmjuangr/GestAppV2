using GestApp.Data.Repositories;
using GestApp.Models;

namespace GestApp.Business.Services
{
    public class PedidoService
    {
        private readonly PedidoRepository _repo;

        public PedidoService()
        {
            _repo = new PedidoRepository();
        }

        public void GuardarPedido(Pedido pedido)
        {
            if (pedido.Productos == null || pedido.Productos.Count == 0)
            {
                throw new ArgumentException("El pedido no puede estar vacío");
            }

            if (pedido.Productos.Any(p => p.PrecioProducto <= 0))
            {
                throw new ArgumentException("Todos los productos deben tener un precio mayor que 0");
            }

            _repo.GuardarPedido(pedido);
        }

        public List<Pedido> ObtenerTodos()
        {
            return _repo.LeerPedidos();
        }

        public List<Pedido> FiltrarPedidos(DateTime? fechaMin, DateTime? fechaMax, bool? confirmado)
        {
            var pedidos = _repo.LeerPedidos();

            if (fechaMin.HasValue)
            {
                pedidos = pedidos
                    .Where(p => p.Fecha >= fechaMin.Value)
                    .ToList();
            }

            if (fechaMax.HasValue)
            {
                pedidos = pedidos
                    .Where(p => p.Fecha <= fechaMax.Value)
                    .ToList();
            }

            if (confirmado.HasValue)
            {
                pedidos = pedidos
                    .Where(p => p.Confirmado == confirmado.Value)
                    .ToList();
            }

            return pedidos;
        }
    }
}
