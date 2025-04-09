using GestApp.Models;

namespace GestApp.Data.Repositories
{
    public class FacturaRepository
    {
        private readonly string _carpetaFacturas = "data/facturas/";

        public FacturaRepository()
        {
            if (!Directory.Exists(_carpetaFacturas))
            {
                Directory.CreateDirectory(_carpetaFacturas);
            }
        }

        public void GuardarFactura(Factura factura)
        {
            string ruta = Path.Combine(_carpetaFacturas, $"factura_{factura.IdFactura}.txt");

            using (StreamWriter sw = new StreamWriter(ruta))
            {
                foreach (var producto in factura.Productos)
                {
                    sw.WriteLine($"{factura.IdFactura}|{factura.IdPedido}|{producto.IdProducto}|{producto.NombreProducto}|{producto.PrecioProducto}");
                }
            }
        }
    }
}
