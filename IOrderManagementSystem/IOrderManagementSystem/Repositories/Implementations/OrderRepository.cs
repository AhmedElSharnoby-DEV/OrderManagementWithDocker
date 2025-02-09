using IOrderManagementSystem.Domain.Context;
using IOrderManagementSystem.Domain.Models;
using IOrderManagementSystem.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace IOrderManagementSystem.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<long> AddOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order.Id;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid order id");
            }
            return await _dbContext.Orders.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
