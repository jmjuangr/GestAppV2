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
            _repo.GuardarPedido(pedido);
        }
    }
}
