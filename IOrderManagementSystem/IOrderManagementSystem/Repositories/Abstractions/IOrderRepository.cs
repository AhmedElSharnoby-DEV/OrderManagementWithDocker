using IOrderManagementSystem.Domain.Models;

namespace IOrderManagementSystem.Repositories.Abstractions
{
    public interface IOrderRepository
    {
        Task<long> AddOrderAsync(Order order);
        Task<List<Order>> GetAllOrders();
        Task<Order?> GetOrderByIdAsync(long id);
    }
}
