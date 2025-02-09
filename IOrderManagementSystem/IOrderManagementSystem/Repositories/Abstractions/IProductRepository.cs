using IOrderManagementSystem.Domain.Models;

namespace IOrderManagementSystem.Repositories.Abstractions
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsListAsync(List<long> ids);
        Task<Product?> GetProductByIdAsync(long id);
    }
}
