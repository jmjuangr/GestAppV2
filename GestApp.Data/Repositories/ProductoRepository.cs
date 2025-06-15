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
            // Devuelve todos los productos guardados en la base de datos
            return _context.Productos.ToList();
        }

        public bool ActualizarProducto(int id, Producto productoActualizado)
        {
            // Busca el producto existente por su ID
            var productoExistente = _context.Productos.Find(id);

            // Si no existe, devuelve false
            if (productoExistente == null)
            {
                return false;
            }

            // Actualiza los campos del producto con los datos nuevos
            productoExistente.NombreProducto = productoActualizado.NombreProducto;
            productoExistente.PrecioProducto = productoActualizado.PrecioProducto;
            productoExistente.Categoria = productoActualizado.Categoria;

            // Guarda los cambios en la base de datos
            _context.SaveChanges();
            return true;
        }

        public bool EliminarProducto(int id)
        {
            // Busca el producto por su ID
            var producto = _context.Productos.Find(id);

            // Si no existe, devuelve false
            if (producto == null)
            {
                return false;
            }

            // Elimina el producto de la base de datos
            _context.Productos.Remove(producto);
            _context.SaveChanges();
            return true;
        }

        public List<Producto> ObtenerPorIds(List<int> ids)
        {
            // Devuelve todos los productos cuyo ID esté dentro de la lista de IDs recibida
            return _context.Productos.Where(p => ids.Contains(p.IdProducto)).ToList();
        }

        public void GuardarProducto(Producto producto)
        {
            // Añade el producto a la base de datos y guarda los cambios
            _context.Productos.Add(producto);
            _context.SaveChanges();
        }
    }
}
