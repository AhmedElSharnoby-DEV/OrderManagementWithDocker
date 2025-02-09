using Microsoft.EntityFrameworkCore;

namespace IOrderManagementSystem.Domain.Common.Abstractions
{
    public interface ISeeder
    {
        void Seed(ModelBuilder modelBuilder);
    }
}
