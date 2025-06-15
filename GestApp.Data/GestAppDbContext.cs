using Microsoft.EntityFrameworkCore;
using GestApp.Models;


namespace GestApp.Data
{
    public class GestAppDbContext : DbContext
    {
        public GestAppDbContext(DbContextOptions<GestAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Factura> Facturas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        //reconocer las claves primarias
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Factura>().HasKey(f => f.IdFactura);
            modelBuilder.Entity<Pedido>().HasKey(p => p.IdPedido);
            modelBuilder.Entity<Producto>().HasKey(p => p.IdProducto);
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
        }
    }
}
