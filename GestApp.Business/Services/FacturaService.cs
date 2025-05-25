using GestApp.Models;
using GestApp.Data.Repositories;

namespace GestApp.Business.Services
{
    public class FacturaService
    {
        private readonly FacturaRepository _repo;

        public FacturaService(FacturaRepository repo)
        {
            _repo = repo;
        }

        public void GuardarFactura(Factura factura)
        {
            if (factura.Productos == null || factura.Productos.Count == 0)
                throw new ArgumentException("La factura no puede estar vacía.");

            if (factura.Productos.Any(p => p.PrecioProducto <= 0))
                throw new ArgumentException("Todos los productos deben tener un precio mayor que 0.");

            _repo.GuardarFactura(factura);
        }

        public List<Factura> ObtenerTodas()
        {
            return _repo.LeerFacturas();
        }

        public List<Factura> FiltrarFacturas(int? idPedido, decimal? importeMin, decimal? importeMax, bool? estaPagada, string? ordenarPor = "importe", bool ascendente = true)
        {
            var facturas = _repo.LeerFacturas();

            if (idPedido.HasValue)
                facturas = facturas.Where(f => f.IdPedido == idPedido.Value).ToList();

            if (importeMin.HasValue)
                facturas = facturas.Where(f => f.ImporteTotal >= importeMin.Value).ToList();

            if (importeMax.HasValue)
                facturas = facturas.Where(f => f.ImporteTotal <= importeMax.Value).ToList();

            if (estaPagada.HasValue)
                facturas = facturas.Where(f => f.EstaPagada == estaPagada.Value).ToList();

            facturas = ordenarPor?.ToLower() switch
            {
                "importe" => ascendente
                    ? facturas.OrderBy(f => f.ImporteTotal).ToList()
                    : facturas.OrderByDescending(f => f.ImporteTotal).ToList(),

                "productos" => ascendente
                    ? facturas.OrderBy(f => f.Productos.Count).ToList()
                    : facturas.OrderByDescending(f => f.Productos.Count).ToList(),

                _ => facturas
            };

            return facturas;
        }

    }
}
