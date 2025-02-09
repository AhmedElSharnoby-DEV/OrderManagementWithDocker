using IOrderManagementSystem.Domain.Context;
using IOrderManagementSystem.Domain.Models;
using IOrderManagementSystem.Repositories.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace IOrderManagementSystem.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(long id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("invalid category id");
            }
            return await _dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
