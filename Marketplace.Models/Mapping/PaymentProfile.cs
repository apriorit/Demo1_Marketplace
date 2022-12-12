using AutoMapper;
using Marketplace.Data.Models.Payments;
using Marketplace.Models.Dto.Payments;

namespace Marketplace.Models.Mapping
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Amount));
        }
    }
}
