using GestApp.Models;

namespace GestApp.Data.Repositories
{
    public class ProductoRepository
    {
        private readonly string _filePath = "data/productos.txt";

        public List<Producto> LeerProductos()
        {
            var productos = new List<Producto>();

            if (!File.Exists(_filePath))
                return productos;

            foreach (var linea in File.ReadAllLines(_filePath))
            {
                var partes = linea.Split('|');

                
                var producto = new Producto(
                    int.Parse(partes[0]),
                    partes[1],
                    decimal.Parse(partes[2])
                );

                productos.Add(producto);
            }

            return productos;
        }
    }
}
