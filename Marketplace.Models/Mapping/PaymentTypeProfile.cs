using AutoMapper;
using Marketplace.Data.Models.Payments;
using Marketplace.Models.Dto.Payments;

namespace Marketplace.Models.Mapping
{
    public class PaymentTypeProfile : Profile
    {
        public PaymentTypeProfile()
        {
            CreateMap<PaymentType, PaymentTypeDto>();
        }
    }
}
