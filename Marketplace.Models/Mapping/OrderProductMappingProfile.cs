using AutoMapper;
using Marketplace.Data.Models.Orders;
using Marketplace.Models.Dto.Orders;
using Marketplace.Models.Dtos.Orders;

namespace Marketplace.Models.Mapping
{
    public class OrderProductMappingProfile : Profile
    {
        public OrderProductMappingProfile()
        {
            CreateMap<OrderProduct, OrderProductDto>()
                .ReverseMap()
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                ;

            CreateMap<OrderProduct, UpdateOrderProductDto>()
                .ReverseMap();
        }
    }
}
