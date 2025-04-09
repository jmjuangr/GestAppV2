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
            _repo.GuardarFactura(factura);
        }
    }
}
