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
            // Añade el pedido a la base de datos y guarda los cambios
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
        }

        public List<Pedido> LeerPedidos()
        {
            // Devuelve todos los pedidos incluyendo los productos asociados a cada uno
            return _context.Pedidos
                .Include(p => p.Productos)
                .ToList();
        }

        public Pedido? ObtenerPorId(int id)
        {
            // Busca un pedido por su ID incluyendo sus productos
            return _context.Pedidos
                .Include(p => p.Productos)
                .FirstOrDefault(p => p.IdPedido == id);
        }

        public bool EliminarPedido(int id)
        {
            // Busca el pedido por su ID e incluye sus productos para poder eliminarlos también
            var pedido = _context.Pedidos
                .Include(p => p.Productos)
                .FirstOrDefault(p => p.IdPedido == id);

            // Si no se encuentra el pedido, devuelve false
            if (pedido == null)
                return false;

            // Elimina los productos asociados al pedido
            _context.Productos.RemoveRange(pedido.Productos);

            // Elimina el pedido en sí
            _context.Pedidos.Remove(pedido);

            // Guarda los cambios en la base de datos
            _context.SaveChanges();

            // Devuelve true indicando que se eliminó correctamente
            return true;
        }
    }
}
