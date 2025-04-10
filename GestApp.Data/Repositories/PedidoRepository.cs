using GestApp.Models;

namespace GestApp.Data.Repositories
{
    public class PedidoRepository
    {
        private readonly string _carpetaPedidos = "data/pedidos/";

        public PedidoRepository()
        {
            // Crear la carpeta si no existe
            if (!Directory.Exists(_carpetaPedidos))
            {
                Directory.CreateDirectory(_carpetaPedidos);
            }
        }

        public void GuardarPedido(Pedido pedido)
        {
            string ruta = Path.Combine(_carpetaPedidos, $"pedido_{pedido.IdPedido}.txt");

            using (StreamWriter sw = new StreamWriter(ruta))
            {
                foreach (var producto in pedido.Productos)
                {
                    sw.WriteLine($"{pedido.IdPedido}|{producto.IdProducto}|{producto.NombreProducto}|{producto.PrecioProducto}");
                }
            }
        }
    
    public List<Pedido> LeerPedidos()
{
    var pedidos = new List<Pedido>();

    if (!Directory.Exists(_carpetaPedidos))
        return pedidos;

    var archivos = Directory.GetFiles(_carpetaPedidos, "*.txt");

    foreach (var archivo in archivos)
    {
        var lineas = File.ReadAllLines(archivo);

        if (lineas.Length == 0)
            continue;

        // Suponemos que todas las líneas son del mismo pedido
        var primerProducto = lineas[0].Split('|');
        int idPedido = int.Parse(primerProducto[0]);

        var pedido = new Pedido(idPedido);

        foreach (var linea in lineas)
        {
            var partes = linea.Split('|');
            var producto = new Producto(
                int.Parse(partes[1]),
                partes[2],
                decimal.Parse(partes[3])
            );
            pedido.AgregarProducto(producto);
        }

        pedidos.Add(pedido);
    }

    return pedidos;
}

    }
}
