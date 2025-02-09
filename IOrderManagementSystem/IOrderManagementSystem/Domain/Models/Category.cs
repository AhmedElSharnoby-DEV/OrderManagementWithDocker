using IOrderManagementSystem.Domain.Common.Model;

namespace IOrderManagementSystem.Domain.Models
{
    public class Category : BaseEntity<long>
    {
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        #region relation 
        public List<Product>? Products { get; set; }
        #endregion
        public Category(long id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
