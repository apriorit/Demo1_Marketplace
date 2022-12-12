namespace Marketplace.Models.Models.Payments
{
    public class PaymentResult
    {
        public string Url { get; set; }
        public string Data { get; set; }
        public string Signature { get; set; }
        public string ClientSecret { get; set; }
    }
}
