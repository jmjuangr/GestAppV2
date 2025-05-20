namespace GestApp.Models;

public class Producto
{
    public int IdProducto { get; set; }
    public string NombreProducto { get; set; }
    public decimal PrecioProducto { get; set; }
    public string Categoria { get; set; }

    public Producto() { }

    public Producto(int idProducto, string nombreProducto, decimal precioProducto)
    {
        IdProducto = idProducto;
        NombreProducto = nombreProducto;
        PrecioProducto = precioProducto;
    }
}
