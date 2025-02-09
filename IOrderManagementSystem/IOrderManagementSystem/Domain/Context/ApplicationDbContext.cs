using IOrderManagementSystem.Domain.Common.Abstractions;
using IOrderManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace IOrderManagementSystem.Domain.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<ProductOrder> ProductOrders { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)"); // Adjust precision and scale as needed

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // add data seeding
            var seeders = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ISeeder).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<ISeeder>();

            foreach(var seeder in seeders)
            {
                seeder.Seed(modelBuilder);
            }
        }
    }
}
