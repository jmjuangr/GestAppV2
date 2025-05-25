using GestApp.Data.Repositories;
using GestApp.Models;

namespace GestApp.Business.Services
{
    public class PedidoService
    {
        private readonly PedidoRepository _repo;

        public PedidoService(PedidoRepository repo)
        {
            _repo = repo;
        }

        public void GuardarPedido(Pedido pedido)
        {
            if (pedido.Productos == null || pedido.Productos.Count == 0)
                throw new ArgumentException("El pedido no puede estar vacío");

            if (pedido.Productos.Any(p => p.PrecioProducto <= 0))
                throw new ArgumentException("Todos los productos deben tener un precio mayor que 0");

            _repo.GuardarPedido(pedido);
        }

        public List<Pedido> ObtenerTodos()
        {
            return _repo.LeerPedidos();
        }

        public List<Pedido> FiltrarPedidos(DateTime? fechaMin, DateTime? fechaMax, bool? confirmado, string? ordenarPor = "fecha", bool ascendente = true)
        {
            var pedidos = _repo.LeerPedidos();

            if (fechaMin.HasValue)
                pedidos = pedidos.Where(p => p.Fecha >= fechaMin.Value).ToList();

            if (fechaMax.HasValue)
                pedidos = pedidos.Where(p => p.Fecha <= fechaMax.Value).ToList();

            if (confirmado.HasValue)
                pedidos = pedidos.Where(p => p.Confirmado == confirmado.Value).ToList();

            pedidos = ordenarPor?.ToLower() switch
            {
                "fecha" => ascendente
                    ? pedidos.OrderBy(p => p.Fecha).ToList()
                    : pedidos.OrderByDescending(p => p.Fecha).ToList(),

                "productos" => ascendente
                    ? pedidos.OrderBy(p => p.Productos.Count).ToList()
                    : pedidos.OrderByDescending(p => p.Productos.Count).ToList(),

                _ => pedidos
            };

            return pedidos;
        }

    }
}
