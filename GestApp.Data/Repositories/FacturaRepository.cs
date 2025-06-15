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
            // Guarda la factura en la base de datos  si  ya existe actualiza 

            _context.Facturas.Update(factura);
            _context.SaveChanges();
        }

        public List<Factura> LeerFacturas()
        {
            // Devuelve facturas 
            return _context.Facturas
                .Include(f => f.Productos)
                .ToList();
        }

        public Factura? ObtenerPorId(int id)
        {
            // Busca una factura por  ID y productos
            return _context.Facturas
                .Include(f => f.Productos)
                .FirstOrDefault(f => f.IdFactura == id);
        }

        public bool EliminarFactura(int id)
        {

            var factura = _context.Facturas.Find(id);


            if (factura == null)
                return false;

            // Si existe elimina 
            _context.Facturas.Remove(factura);
            _context.SaveChanges();


            return true;
        }
    }
}
