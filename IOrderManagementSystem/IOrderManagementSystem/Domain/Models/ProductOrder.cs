using IOrderManagementSystem.Domain.Common.Model;
using IOrderManagementSystem.Domain.Dtos;

namespace IOrderManagementSystem.Domain.Models
{
    public class ProductOrder : BaseEntity<long>
    {
        public long ProductId { get; private set; }
        public Product Product { get; private set; } = null!;
        public long OrderId { get; private set; }
        public Order Order { get; private set; } = null!;
        public int Quantity { get; private set; }
        public static ProductOrder Create(ProductOrderDto createProductOrderDto)
        {
            return new ProductOrder()
            {
                Product = createProductOrderDto.Product,
                Order = createProductOrderDto.Order,
                Quantity = createProductOrderDto.Quantity
            };
        }
    }
}
