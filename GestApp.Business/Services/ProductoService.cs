using GestApp.Data.Repositories;
using GestApp.Models;

namespace GestApp.Business.Services
{
    public class ProductoService
    {
        private readonly ProductoRepository _repo;

        public ProductoService()
        {
            _repo = new ProductoRepository();
        }

        public List<Producto> ObtenerTodos()
        {
            return _repo.LeerProductos();
        }

        public List<Producto> FiltrarProductos(string? nombre, decimal? precioMin, decimal? precioMax)
{
    var productos = _repo.LeerProductos();

    if (!string.IsNullOrEmpty(nombre))
    {
        productos = productos
            .Where(p => p.NombreProducto.ToLower().Contains(nombre.ToLower()))
            .ToList();
    }

    if (precioMin.HasValue)
    {
        productos = productos
            .Where(p => p.PrecioProducto >= precioMin.Value)
            .ToList();
    }

    if (precioMax.HasValue)
    {
        productos = productos
            .Where(p => p.PrecioProducto <= precioMax.Value)
            .ToList();
    }

    return productos;
}

    }
}
