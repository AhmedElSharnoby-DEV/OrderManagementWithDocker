namespace IOrderManagementSystem.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        public string ShippingAddress { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string CustomerPhoneNumber { get; set; } = null!;
        public DateTime ArrivalDate { get; set; }
        public List<CreateProductOrderDto> Products { get; set; } = new();
    }
}
