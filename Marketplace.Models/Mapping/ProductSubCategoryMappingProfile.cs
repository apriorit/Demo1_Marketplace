
using AutoMapper;
using Marketplace.Data.Models.Stocks;
using Marketplace.Models.Dto.Stock;

namespace Marketplace.Models.Mapping
{
    public class ProductSubCategoryMappingProfile : Profile
    {
        public ProductSubCategoryMappingProfile()
        {
            CreateMap<ProductSubCategory, ProductSubCategoryDto>()
                .ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());
        }
    }
}
