using Marketplace.Models.Enum.Payment;

namespace Marketplace.Contracts.Payments
{
    public interface IPaymentProvider
    {
        IPaymentIntegrationSystem GetPaymentSystem(PaymentSystemType paymentSystemType);
    }
}
