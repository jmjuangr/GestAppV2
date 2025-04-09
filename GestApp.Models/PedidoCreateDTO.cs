namespace GestApp.Models
{
    public class PedidoCreateDTO
    {
        public int IdPedido { get; set; }
        public List<Producto> Productos { get; set; }
    }
}
