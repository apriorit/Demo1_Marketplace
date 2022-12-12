using Autofac.Features.Indexed;
using Marketplace.Contracts.Payments;
using Marketplace.Models.Enum.Payment;

namespace Marketplace.Services.Payments
{
    public class PaymentProvider : IPaymentProvider
    {
        private readonly IIndex<string, IPaymentIntegrationSystem> _serviceIndexes;

        public PaymentProvider(IIndex<string, IPaymentIntegrationSystem> serviceIndexes)
        {
            _serviceIndexes = serviceIndexes;
        }

        public IPaymentIntegrationSystem GetPaymentSystem(PaymentSystemType paymentSystemType)
        {
            switch (paymentSystemType)
            {
                case PaymentSystemType.Stripe:
                    return _serviceIndexes["Stripe"];
                case PaymentSystemType.LiqPay:
                default:
                    return _serviceIndexes["LiqPay"];
            }
        }
    }
}
