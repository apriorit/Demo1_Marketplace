using AutoMapper;
using Marketplace.Data.Models.Stocks;
using Marketplace.Models.Dto.Stock;

namespace Marketplace.Models.Mapping
{
    internal class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDto>()
               .ReverseMap()
               .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
