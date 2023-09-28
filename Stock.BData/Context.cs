using Microsoft.EntityFrameworkCore;
using Stock.BData.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Stock.BData
{
    public class Context : DbContext
    {
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Venta> Ventas => Set<Venta>();
        public Context(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(o =>
            {
                o.Property(b => b.Id);
                o.Property(b => b.NombreProducto);
                o.Property(b => b.PrecioProducto).HasColumnType("Decimal(10,2)");
                o.Property(b => b.Stock);
            });
            modelBuilder.Entity<Venta>(o => { 
                o.Property(b =>b.Id);
                o.Property(b => b.ProductoNombre);
                o.Property(b => b.CodProducto);
                o.Property(b => b.Precio).HasColumnType("Decimal(10,2)");
                o.Property(b => b.FechaVenta);
                o.Property(b => b.Cantidad);
                o.HasOne(b => b.Producto);
            });
            
        }
    }
}
