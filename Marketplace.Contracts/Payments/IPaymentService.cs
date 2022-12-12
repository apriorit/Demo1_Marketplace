using Marketplace.Models.Dtos.Payments;
using Marketplace.Models.Enum.Payment;
using Marketplace.Models.Models.Payments;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Payments
{
    public interface IPaymentService
    {
        Task<PaymentStatusType> ReceiveResponseAsync(string data, string signature);
        Task<PaymentStatus> GetPaymentStatusAsync(int orderId);
        Task<PaymentResult> RequestPaymentAsync(CreatePaymentDto createPaymentModel);
    }
}
