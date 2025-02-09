using IOrderManagementSystem.Dtos.CategoryDtos;

namespace IOrderManagementSystem.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<List<GetCategoryDto>> GetAllCategories();
        Task<GetCategoryDto?> GetCategoryById(long id);
    }
}
