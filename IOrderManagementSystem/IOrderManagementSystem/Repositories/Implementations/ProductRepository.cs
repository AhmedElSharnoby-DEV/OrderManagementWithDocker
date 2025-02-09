using IOrderManagementSystem.Domain.Context;
using IOrderManagementSystem.Domain.Models;
using IOrderManagementSystem.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace IOrderManagementSystem.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid product id");
            }
            return await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetProductsListAsync(List<long> ids)
        {
            if(ids is null || ids.Any(x => x < 0))
            {
                throw new ArgumentException("invalid product ids");
            }
            return await _dbContext.Products.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}
