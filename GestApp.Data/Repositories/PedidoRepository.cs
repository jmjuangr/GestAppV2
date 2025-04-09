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
    }
}
