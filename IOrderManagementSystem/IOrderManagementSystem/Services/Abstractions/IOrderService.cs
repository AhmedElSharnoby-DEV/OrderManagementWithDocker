using IOrderManagementSystem.Dtos.OrderDtos;

namespace IOrderManagementSystem.Services.Abstractions
{
    public interface IOrderService
    {
        Task<long> CreateOrder(CreateOrderDto createOrderDto);
    }
}
