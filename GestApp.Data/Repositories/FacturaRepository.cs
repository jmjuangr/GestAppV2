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

        public List<Factura> LeerFacturas()
{
    var facturas = new List<Factura>();

    if (!Directory.Exists(_carpetaFacturas))
        return facturas;

    var archivos = Directory.GetFiles(_carpetaFacturas, "*.txt");

    foreach (var archivo in archivos)
    {
        var lineas = File.ReadAllLines(archivo);

        if (lineas.Length == 0)
            continue;

        var primerLinea = lineas[0].Split('|');
        int idFactura = int.Parse(primerLinea[0]);
        int idPedido = int.Parse(primerLinea[1]);

        var productos = new List<Producto>();

        foreach (var linea in lineas)
        {
            var partes = linea.Split('|');
            var producto = new Producto(
                int.Parse(partes[2]),
                partes[3],
                decimal.Parse(partes[4])
            );
            productos.Add(producto);
        }

        var factura = new Factura(idFactura, idPedido, productos);
        facturas.Add(factura);
    }

    return facturas;
}


    }
}
