using IOrderManagementSystem.Domain.Models;

namespace IOrderManagementSystem.Repositories.Abstractions
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(long id); 
    }
}
