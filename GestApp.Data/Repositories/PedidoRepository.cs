using GestApp.Models;
using GestApp.Data;
using Microsoft.EntityFrameworkCore;

namespace GestApp.Data.Repositories
{
    public class PedidoRepository
    {
        private readonly GestAppDbContext _context;

        public PedidoRepository(GestAppDbContext context)
        {
            _context = context;
        }

        public void GuardarPedido(Pedido pedido)
        {
            // Añade el pedido
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
        }

        public List<Pedido> LeerPedidos()
        {
            // Devuelve los pedidos
            return _context.Pedidos
                .Include(p => p.Productos)
                .ToList();
        }

        public Pedido? ObtenerPorId(int id)
        {
            // Busca un pedido 
            return _context.Pedidos
                .Include(p => p.Productos)
                .FirstOrDefault(p => p.IdPedido == id);
        }

        public bool EliminarPedido(int id)
        {
            // Busca el pedido  incluye los productos para eliminar
            var pedido = _context.Pedidos
                .Include(p => p.Productos)
                .FirstOrDefault(p => p.IdPedido == id);


            if (pedido == null)
                return false;


            _context.Productos.RemoveRange(pedido.Productos);


            _context.Pedidos.Remove(pedido);


            _context.SaveChanges();

            return true;
        }
    }
}
