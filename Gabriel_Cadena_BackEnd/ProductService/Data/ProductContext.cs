using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "Laptop de última generación", Category = "Electrónicos", Price = 1200.50m, Stock = 10 },
                new Product { Id = 2, Name = "Teléfono", Description = "Teléfono inteligente", Category = "Electrónicos", Price = 800.00m, Stock = 25 }
            );
        }
    }
}