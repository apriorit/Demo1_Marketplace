using Marketplace.Models.Dtos.Payments;
using Marketplace.Models.Models.Payments;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Payments
{
    public interface IPaymentIntegrationSystem
    {
        PaymentResultDto RequestPayment(PaymentInfoDto paymentInfo);
        Task<PaymentStatus> GetPaymentStatusAsync(int orderId);
        PaymentStatus DecodePaymentResponse(string data);
    }
}
