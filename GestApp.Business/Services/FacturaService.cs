using GestApp.Models;
using GestApp.Data.Repositories;

namespace GestApp.Business.Services
{
    public class FacturaService
    {
        private readonly FacturaRepository _repo;
        private readonly PedidoRepository _pedidoRepo;

        public FacturaService(FacturaRepository repo, PedidoRepository pedidoRepo)
        {
            _repo = repo;
            _pedidoRepo = pedidoRepo;
        }

        public Factura GenerarFacturaDesdePedido(int idPedido)
        {
            var pedido = _pedidoRepo.ObtenerPorId(idPedido);
            if (pedido == null)
                throw new ArgumentException("El pedido no existe.");

            if (pedido.Productos == null || !pedido.Productos.Any())
                throw new ArgumentException("El pedido no tiene productos.");

            var factura = new Factura(0, idPedido, pedido.Productos)
            {
                EstaPagada = false
            };

            _repo.GuardarFactura(factura);
            return factura;
        }

        public List<Factura> ObtenerTodas()
        {
            return _repo.LeerFacturas();
        }

        public Factura? ObtenerPorId(int id)
        {
            return _repo.ObtenerPorId(id);
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

        public bool MarcarComoPagada(int idFactura)
        {
            var factura = _repo.ObtenerPorId(idFactura);
            if (factura == null)
                return false;

            factura.EstaPagada = true;
            _repo.GuardarFactura(factura);
            return true;
        }

        public bool EliminarFactura(int id)
        {
            return _repo.EliminarFactura(id);
        }
    }
}
