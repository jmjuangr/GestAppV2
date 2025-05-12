using System;
using System.Collections.Generic;

namespace GestApp.Models
{
    public class Factura
    {

        public int IdFactura { get; set; }
        public int IdPedido { get; set; }
        public List<Producto> Productos { get; set; }
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
            decimal total = 0;
            foreach (var producto in Productos)
            {

                total += producto.PrecioProducto;
            }
            return total;
        }


        public void MostrarDetalles()
        {
            Console.WriteLine($"Factura N°: {IdFactura}, Pedido N°: {IdPedido}");
            Console.WriteLine("Productos en la factura:");
            foreach (var producto in Productos)
            {
                producto.MostrarDetalles();
            }
            Console.WriteLine($"Importe Total: {ImporteTotal:C}");
        }


        public void GuardarFactura(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
                foreach (var producto in Productos)
                {
                    sw.WriteLine($"{IdFactura}|{IdPedido}|{producto.IdProducto}|{producto.NombreProducto}|{producto.PrecioProducto}");
                }
            Console.WriteLine($"Factura guardada correctamente en {filePath}.");
        }


    }
}





