using IOrderManagementSystem.Dtos.ProductDtos;

namespace IOrderManagementSystem.Services.Abstractions
{
    public interface IProductService
    {
        Task<List<GetProductDto>> GetAllProductsAsync();
        Task<GetProductDto?> GetProductByIdAsync(long id);
    }
}
