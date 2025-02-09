using IOrderManagementSystem.Domain.Common.Model;

namespace IOrderManagementSystem.Domain.Models
{
    public class Product : BaseEntity<long>
    {
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public decimal Price { get; private set; }
        public long StockQuantity { get; private set; }
        #region relation 
        public long CategoryId { get; private set; }
        public Category Category { get; private set; } = null!;
        public List<Order>? Order { get; private set; }
        #endregion
        public Product(long id, string name , string description, decimal price, long stockQuantity, long categoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            CategoryId = categoryId;
        }
    }
}
