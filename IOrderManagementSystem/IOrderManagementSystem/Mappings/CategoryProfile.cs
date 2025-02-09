using AutoMapper;
using IOrderManagementSystem.Domain.Models;
using IOrderManagementSystem.Dtos.CategoryDtos;

namespace IOrderManagementSystem.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoryDto>();
        }
    }
}
