using System;

namespace GestApp.Models;

public class Pedido
{
    public int IdPedido { get; set; }
    public DateTime Fecha { get; set; }
    public List<Producto> Productos { get; set; } = new();
    public bool Confirmado { get; set; }

    public Pedido() { }

    public Pedido(int idPedido)
    {
        IdPedido = idPedido;
        Fecha = DateTime.Now;
    }
}
