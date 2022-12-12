using AutoMapper;
using Marketplace.Data.Models;
using Marketplace.Models.Dtos;

namespace Marketplace.Models.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
