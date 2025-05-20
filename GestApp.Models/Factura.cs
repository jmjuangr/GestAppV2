namespace GestApp.Models;

public class Factura
{
    public int IdFactura { get; set; }
    public int IdPedido { get; set; }
    public List<Producto> Productos { get; set; } = new();
    public decimal ImporteTotal { get; set; }
    public bool EstaPagada { get; set; }

    public Factura() { }

    public Factura(int idFactura, int idPedido, List<Producto> productos)
    {
        IdFactura = idFactura;
        IdPedido = idPedido;
        Productos = productos;
        ImporteTotal = CalcularImporteTotal();
    }

    public decimal CalcularImporteTotal()
    {
        return Productos.Sum(p => p.PrecioProducto);
    }
}
