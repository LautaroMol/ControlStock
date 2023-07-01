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
        public DbSet<Productos> Producto => Set<Productos>();
        public DbSet<Informe> Informes => Set<Informe>();
        public Context(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Productos>(o =>
            {
                o.Property(b => b.PrecioProducto).HasColumnType("Decimal(10,2)");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
