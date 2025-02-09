using AutoMapper;
using IOrderManagementSystem.Dtos.CategoryDtos;
using IOrderManagementSystem.Repositories.Abstractions;
using IOrderManagementSystem.Services.Abstractions;

namespace IOrderManagementSystem.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<List<GetCategoryDto>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<List<GetCategoryDto>>(categories);
        }

        public async Task<GetCategoryDto?> GetCategoryById(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid category id");
            }
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            return _mapper.Map<GetCategoryDto>(category);
        }
    }
}
