using IOrderManagementSystem.Domain.Common.Model;
using IOrderManagementSystem.Dtos.OrderDtos;

namespace IOrderManagementSystem.Domain.Models
{
    public class Order : BaseEntity<long>
    {
        public decimal? TotalPrice { get; private set; }
        public string ShippingAddress { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string CustomerPhoneNumber { get; private set; } = null!;
        public DateTime ArrivalDate { get; private set; }
        public bool? IsCustomerNotified { get; private set; }
        public string? NotificationMessage { get; private set; }
        public DateTime CreatedAt { get; private set; }
        #region relation 
        public List<ProductOrder> OrderProducts { get; set; }
        #endregion
        private Order()
        {
            OrderProducts = new();
            CreatedAt = DateTime.Now;
        }
        private Order(string shippingAddress, string email, string customerPhoneNumber, DateTime arrivalDate)
            :this()
        {
            ShippingAddress = shippingAddress;
            Email = email;
            CustomerPhoneNumber = customerPhoneNumber;
            ArrivalDate = arrivalDate;
        }
        public static Order CreateOrder(CreateOrderDto orderDto)
        {
            return new Order(
                orderDto.ShippingAddress,
                orderDto.Email,
                orderDto.CustomerPhoneNumber,
                orderDto.ArrivalDate);
        }
        public void MarkCustomerNotified(string notificationMessage)
        {
            IsCustomerNotified = true;
            NotificationMessage = notificationMessage;
        }
        public void AddOrderTotalPrice(decimal totalPrice)
        {
            TotalPrice = totalPrice;
        }
    }
}
