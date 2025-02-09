using AutoMapper;
using IOrderManagementSystem.Domain.Models;
using IOrderManagementSystem.Dtos.ProductDtos;

namespace IOrderManagementSystem.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductDto>();
        }
    }
}
