using GestApp.Models;
using GestApp.Data.Repositories;

namespace GestApp.Business.Services
{
    public class FacturaService
    {
        private readonly FacturaRepository _repo;

        public FacturaService()
                {
                    _repo = new FacturaRepository();
                }

       public void GuardarFactura(Factura factura)
                {
                    if (factura.Productos == null || factura.Productos.Count == 0)
                    {
                        throw new ArgumentException("La factura no puede estar vacía.");
                    }

                    if (factura.Productos.Any(p => p.PrecioProducto <= 0))
                    {
                        throw new ArgumentException("Todos los productos deben tener un precio mayor que 0.");
                    }

                    _repo.GuardarFactura(factura);
                }
    public List<Factura> ObtenerTodas()
                {
                    return _repo.LeerFacturas();
                }

    }
}
