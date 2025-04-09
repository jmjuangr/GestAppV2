namespace GestApp.Models
{
    public class FacturaCreateDTO
    {
        public int IdFactura { get; set; }
        public int IdPedido { get; set; }
        public List<Producto> Productos { get; set; }
    }
}
