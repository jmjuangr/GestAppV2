using System.Runtime.Intrinsics.X86;

namespace GestApp.Models;
public class Pedido

{


    public int IdPedido { get; set; }
    public DateTime Fecha { get; set; }
    public List<Producto> Productos { get; set; }
    public bool Confirmado { get; set; }

    public Pedido() { }

    public Pedido(int idPedido)
    {
        IdPedido = idPedido;
        Fecha = DateTime.Now;
        Productos = new List<Producto>();
    }


    public void AgregarProducto(Producto producto)
    {
        Productos.Add(producto);
    }

    public void MostrarDetalles()
    {
        Console.WriteLine($"Pedido N°: {IdPedido}, Fecha: {Fecha}");
        Console.WriteLine("Productos en el pedido:");

        if (Productos.Count == 0)
        {
            Console.WriteLine("No hay productos en este pedido.");
        }
        else
        {
            foreach (var producto in Productos)
            {
                producto.MostrarDetalles();
            }
        }
    }



    public void GuardarPedido(string filePath)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            foreach (var producto in Productos)
            {
                sw.WriteLine($"{IdPedido}|{producto.IdProducto}|{producto.NombreProducto}|{producto.PrecioProducto}");
            }
        }

        Console.WriteLine($"Pedido guardado correctamente en {filePath}.");
    }



}


