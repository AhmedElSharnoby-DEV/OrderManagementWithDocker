using IOrderManagementSystem.Domain.Common.Abstractions;
using IOrderManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace IOrderManagementSystem.Domain.Seeding
{
    public class CategoryDataSeeding : ISeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category(1, "Category 1", "This category related to foods"),
                new Category(2, "Category 2", "This category related to electronics"),
                new Category(3, "Category 3", "This category related to clothes"),
                new Category(4, "Category 4", "This category related to medicals")
                );
        }
    }
}
