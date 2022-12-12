using AutoMapper;
using Marketplace.Data.Models.Stocks;
using Marketplace.Models.Dto.Products;

namespace Marketplace.Models.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductSubCategory, opt => opt.MapFrom(src => src.ProductSubCategory))
                .ReverseMap()
                .ForMember(dest => dest.ProductSubCategory, opt => opt.MapFrom(src => src.ProductSubCategory))
                .ForMember(dest => dest.ProductSubCategoryId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());
        }
    }
}
