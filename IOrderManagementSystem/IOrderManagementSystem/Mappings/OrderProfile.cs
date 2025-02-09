using AutoMapper;
using IOrderManagementSystem.Domain.Models;
using IOrderManagementSystem.Dtos.OrderDtos;

namespace IOrderManagementSystem.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderNotificationMessageDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.OrderProducts));

            CreateMap<ProductOrder, OrderProductsDto>()
                .ForPath(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

        }
    }
}
