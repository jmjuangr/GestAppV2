using GestApp.Data.Repositories;
using GestApp.Models;

namespace GestApp.Business.Services
{
    public class ProductoService
    {
        private readonly ProductoRepository _repo;

        public ProductoService(ProductoRepository repo)
        {
            _repo = repo;
        }

        public List<Producto> ObtenerTodos()
        {
            return _repo.LeerProductos();
        }

        public List<Producto> FiltrarProductos(string? nombre, decimal? precioMin, decimal? precioMax, string? ordenarPor = "nombre", bool ascendente = true)
        {
            var productos = _repo.LeerProductos();

            if (!string.IsNullOrEmpty(nombre))
            {
                productos = productos
                    .Where(p => p.NombreProducto.ToLower().Contains(nombre.ToLower()))
                    .ToList();
            }

            if (precioMin.HasValue)
                productos = productos.Where(p => p.PrecioProducto >= precioMin.Value).ToList();

            if (precioMax.HasValue)
                productos = productos.Where(p => p.PrecioProducto <= precioMax.Value).ToList();

            productos = ordenarPor?.ToLower() switch
            {
                "precio" => ascendente
                    ? productos.OrderBy(p => p.PrecioProducto).ToList()
                    : productos.OrderByDescending(p => p.PrecioProducto).ToList(),

                _ => ascendente
                    ? productos.OrderBy(p => p.NombreProducto).ToList()
                    : productos.OrderByDescending(p => p.NombreProducto).ToList()
            };

            return productos;
        }


        public bool ActualizarProducto(int id, ProductoCreateDTO dto)
        {
            var producto = new Producto
            {
                NombreProducto = dto.NombreProducto,
                PrecioProducto = dto.PrecioProducto,
                Categoria = dto.Categoria
            };

            return _repo.ActualizarProducto(id, producto);
        }


        public bool EliminarProducto(int id)
        {
            return _repo.EliminarProducto(id);
        }

        public void CrearProducto(ProductoCreateDTO dto)
        {
            var nuevoProducto = new Producto
            {
                NombreProducto = dto.NombreProducto,
                PrecioProducto = dto.PrecioProducto,
                Categoria = dto.Categoria
            };

            _repo.GuardarProducto(nuevoProducto);
        }




    }
}
