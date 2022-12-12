using AutoMapper;
using Marketplace.Data.Models.Stocks;
using Marketplace.Models.Dto;

namespace Marketplace.Models.Mapping
{
    public class PriceMappingProfile : Profile
    {
        public PriceMappingProfile()
        {
            CreateMap<Price, PriceDto>()
                .ReverseMap()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());
        }
    }
}
