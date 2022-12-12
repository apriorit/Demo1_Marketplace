using Marketplace.Models.Enum.Payment;

namespace Marketplace.Models.Dtos.Payments
{
    public class CreatePaymentDto
    {
        public RequestActionType Action { get; set; }
        public Currency Currency { get; set; }
        public PaymentSystemType PaymentSystemType { get; set; }
        public int OrderId { get; set; }
        public string Description { get; set; }
    }
}
