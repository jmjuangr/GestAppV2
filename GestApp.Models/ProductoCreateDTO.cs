namespace GestApp.Models
{
    public class ProductoCreateDTO
    {
        public string NombreProducto { get; set; } = string.Empty;
        public decimal PrecioProducto { get; set; }
        public string Categoria { get; set; } = string.Empty;
    }
}
