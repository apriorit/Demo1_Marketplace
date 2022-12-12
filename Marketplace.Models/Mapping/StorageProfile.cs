using AutoMapper;
using Marketplace.Data.Models.Stocks;
using Marketplace.Models.Dto;

namespace Marketplace.Models.Mapping
{
    internal class StorageProfile : Profile
    {
        public StorageProfile()
        {
            CreateMap<Storage, StorageDto>()
               .ReverseMap()
               .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
