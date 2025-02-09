
using IOrderManagementSystem.Domain.Models;

namespace IOrderManagementSystem.Domain.Dtos
{
    public class ProductOrderDto
    {
        public Product Product { get; set; } = null!;
        public Order Order { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
