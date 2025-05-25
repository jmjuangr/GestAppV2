using GestApp.Models;
using GestApp.Data;
using Microsoft.EntityFrameworkCore;

namespace GestApp.Data.Repositories
{
    public class FacturaRepository
    {
        private readonly GestAppDbContext _context;

        public FacturaRepository(GestAppDbContext context)
        {
            _context = context;
        }

        public void GuardarFactura(Factura factura)
        {
            _context.Facturas.Add(factura);
            _context.SaveChanges();
        }

        public List<Factura> LeerFacturas()
        {
            return _context.Facturas
                .Include(f => f.Productos)
                .ToList();
        }

        public Factura? ObtenerPorId(int id)
        {
            return _context.Facturas
                .Include(f => f.Productos)
                .FirstOrDefault(f => f.IdFactura == id);
        }
    }
}
