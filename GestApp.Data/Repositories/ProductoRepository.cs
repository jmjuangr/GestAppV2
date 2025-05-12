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
    }
}
