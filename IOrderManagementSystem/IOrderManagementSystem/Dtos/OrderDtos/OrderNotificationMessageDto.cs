namespace IOrderManagementSystem.Dtos.OrderDtos
{
    public class OrderNotificationMessageDto
    {
        public string ShippingAddress { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime ArrivalDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderProductsDto> Products { get; set; } = new();
    }
}
