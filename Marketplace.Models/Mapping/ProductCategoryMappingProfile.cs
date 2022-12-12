using AutoMapper;
using Marketplace.Data.Models.Stocks;
using Marketplace.Models.Dto;

namespace Marketplace.Models.Mapping
{
    internal class ProductCategoryMappingProfile : Profile
    {
        public ProductCategoryMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>()
               .ReverseMap()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
               .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore());
        }
    }
}
