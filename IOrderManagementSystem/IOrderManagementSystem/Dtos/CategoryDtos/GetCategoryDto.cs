namespace IOrderManagementSystem.Dtos.CategoryDtos
{
    public class GetCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
