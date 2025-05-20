using GestApp.Models;
using GestApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace GestApp.Data.Repositories
{
    public class ProductoRepository
    {
        private readonly GestAppDbContext _context;

        public ProductoRepository(GestAppDbContext context)
        {
            _context = context;
        }

        public List<Producto> LeerProductos()
        {
            return _context.Productos.ToList();
        }

        public bool ActualizarProducto(int id, Producto productoActualizado)
        {
            var productoExistente = _context.Productos.Find(id);

            if (productoExistente == null)
            {
                return false;
            }

            productoExistente.NombreProducto = productoActualizado.NombreProducto;
            productoExistente.PrecioProducto = productoActualizado.PrecioProducto;
            productoExistente.Categoria = productoActualizado.Categoria;

            _context.SaveChanges();
            return true;
        }

        public bool EliminarProducto(int id)
        {
            var producto = _context.Productos.Find(id);

            if (producto == null)
            {
                return false;
            }

            _context.Productos.Remove(producto);
            _context.SaveChanges();
            return true;
        }

        public void GuardarProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
        }




    }
}
