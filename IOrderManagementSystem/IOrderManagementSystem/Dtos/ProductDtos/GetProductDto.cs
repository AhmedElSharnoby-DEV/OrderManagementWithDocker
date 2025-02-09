namespace IOrderManagementSystem.Dtos.ProductDtos
{
    public class GetProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public long StockQuantity { get; set; }
    }
}
