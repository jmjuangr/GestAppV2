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
    }
}
