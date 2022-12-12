using AutoMapper;
using Marketplace.Data.Models;
using Marketplace.Models.Dtos;

namespace Marketplace.Models.Mapping
{
    internal class ElasticMappingProfile : Profile
    {
        public ElasticMappingProfile()
        {
            CreateMap<ElasticEntity, ElasticDto>()
               .ReverseMap()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
               .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore());
        }
    }
}
