using Marketplace.Models.Enum.Payment;

namespace Marketplace.Models.Dtos.Payments
{
    public class PaymentInfoDto
    {
        public double Amount { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; }
        public Language Language { get; set; }
        public RequestActionType Action { get; set; }
    }
}
