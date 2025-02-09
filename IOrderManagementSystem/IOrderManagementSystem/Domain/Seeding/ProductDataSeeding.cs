using IOrderManagementSystem.Domain.Common.Abstractions;
using IOrderManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace IOrderManagementSystem.Domain.Seeding
{
    public class ProductDataSeeding : ISeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product(1,"Product 1", "Product 1 description", 12, 100, 1),
                new Product(2,"Product 2", "Product 2 description", 13, 100, 1),
                new Product(3,"Product 3", "Product 3 description", 14, 100, 1),
                new Product(4,"Product 4", "Product 4 description", 15, 100, 2),
                new Product(5,"Product 5", "Product 5 description", 16, 100, 2),
                new Product(6,"Product 6", "Product 6 description", 17, 100, 2),
                new Product(7,"Product 7", "Product 7 description", 18, 100, 3),
                new Product(8,"Product 8", "Product 8 description", 19, 100, 3),
                new Product(9,"Product 9", "Product 9 description", 20, 100, 3),
                new Product(10,"Product 10", "Product 10 description", 21, 100, 4),
                new Product(11,"Product 11", "Product 11 description", 22, 100, 4),
                new Product(12,"Product 12", "Product 12 description", 23, 100, 4),
                new Product(13,"Product 13", "Product 13 description", 24, 100, 4)
            );
        }
    }
}
