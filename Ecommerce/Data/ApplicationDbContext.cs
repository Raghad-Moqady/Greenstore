using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=EcommerceMVC;Trusted_Connection=True;TrustServerCertificate=True");
        }
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Outdoor Plants", Image = "cat1.jpg" },
                new Category { Id = 2, Name = "Houseplants", Image = "cat2.jpg" },
                new Category { Id = 3, Name = "Succulents", Image = "cat3.jpg" },
                new Category { Id = 4, Name = "Desert Bloom", Image = "cat4.jpg" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Desert Bloom", CategoryId = 1, Description = "This is Desert Bloom", Image = "product1.jpg", Price = 60m, Quantity = 10 },
                new Product { Id = 2, Name = "Golden Glow", CategoryId = 2, Description = "This is Golden Glow", Image = "product2.jpg", Price = 85m, Quantity = 5 },
                new Product { Id = 3, Name = "Silver Mist", CategoryId = 4, Description = "This is Silver Mist", Image = "product3.jpg", Price = 100m, Quantity = 20 },
                new Product { Id = 4, Name = "Zen Bamboo Grove", CategoryId = 2, Description = "This is Zen Bamboo Grove", Image = "product4.jpg", Price = 10.5m, Quantity = 1 },
                new Product { Id = 5, Name = "Tropical Breeze", CategoryId = 3, Description = "This is Tropical Breeze", Image = "product5.jpg", Price = 200m, Quantity = 8 }
            );
        }

    }
}
