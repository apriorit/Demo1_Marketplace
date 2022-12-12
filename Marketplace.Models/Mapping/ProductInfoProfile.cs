using AutoMapper;
using Marketplace.Data.Models.Stocks;
using Marketplace.Models.Dto;

namespace Marketplace.Models.Mapping
{
    internal class ProductInfoProfile : Profile
    {
        public ProductInfoProfile()
        {
            CreateMap<ProductInfo, ProductInfoDto>()
               .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
               .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
               .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());
        }
    }
}
