using AutoMapper;
using Marketplace.Integrations.Payments.LiqPay.Models;
using Marketplace.Models.Models.Payments;

namespace Marketplace.Integrations.Payments.LiqPay.Mapping
{
    internal class LiqPayResponseMappingProfile : Profile
    {
        public LiqPayResponseMappingProfile()
        {
            CreateMap<LiqPayResponse, PaymentStatus>();
        }
    }
}
