using System;
namespace GestApp.Models;

public class Producto {

public int IdProducto {get;set;}
public string NombreProducto {get;set;}
public decimal PrecioProducto {get;set;}


public Producto (int idProducto, string nombreProducto, decimal precioProducto){

    IdProducto=idProducto;
    NombreProducto=nombreProducto;
    PrecioProducto=precioProducto;

}



    public void MostrarDetalles()
        {
            Console.WriteLine($"ID: {IdProducto}, Nombre: {NombreProducto}, Precio:{PrecioProducto}");
        }

    
}