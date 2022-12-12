using Marketplace.Contracts.Payments;
using Marketplace.Integrations.Payments.Stripe.Common;
using Marketplace.Integrations.Payments.Stripe.Models;
using Marketplace.Models.Dtos.Payments;
using Marketplace.Models.Enum.Payment;
using Marketplace.Models.Models.Payments;
using Stripe;

namespace Marketplace.Integrations.Payments.Stripe.Services
{
    public class StripePaymentService : IPaymentIntegrationSystem
    {
        private readonly string _publicKey;
        private readonly string _privateKey;
        private readonly string _returnURL;

        public StripePaymentService(IConfiguration config)
        {
            _publicKey = config.GetValue<string>("Stripe:PublicKey");
            _privateKey = config.GetValue<string>("Stripe:PrivateKey");
            _returnURL = config.GetValue<string>("Stripe:ReturnURL");

            if (string.IsNullOrEmpty(_publicKey) || string.IsNullOrEmpty(_privateKey))
            {
                throw new ArgumentException("Stripe credentials are not valid");
            }

            StripeConfiguration.ApiKey = _privateKey;
        }

        public PaymentStatus DecodePaymentResponse(string data)
        {
            throw new NotSupportedException("Stripe doesn't need to decode responses");
        }

        public async Task<PaymentStatus> GetPaymentStatusAsync(int orderId)
        {
            var options = new PaymentIntentSearchOptions
            {
                Query = $"metadata['OrderId']:'{orderId}'",
            };
            var service = new PaymentIntentService();
            var paymentIntent = (await service.SearchAsync(options)).FirstOrDefault();
            if (paymentIntent == null)
            {
                return null;
            }
            else
            {
                var paymentStatus = new PaymentStatus() { 
                    Action = ResponseActionType.Pay,
                    Amount = StripeUtil.ToDouble(paymentIntent.Amount),
                    CreateDate = paymentIntent.Created,
                    Currency = (Currency)Enum.Parse(typeof(Currency), paymentIntent.Currency.ToUpperInvariant()),
                    CustomerId = paymentIntent.CustomerId, 
                    Description = paymentIntent.Description,
                    ErrorDescription = paymentIntent.CancellationReason,
                    PaymentOrderId = paymentIntent.Id,
                    OrderId = orderId.ToString()
                };

                return paymentStatus;
            }
        }

        public PaymentResultDto RequestPayment(PaymentInfoDto paymentInfo)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = StripeUtil.ToLong(paymentInfo.Amount),
                Currency = Enum.GetName(typeof(Currency), paymentInfo.Currency),
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
                Metadata = new Dictionary<string, string>
                {
                    {"OrderId", paymentInfo.OrderId.ToString()},
                }, 
            };

            var service = new PaymentIntentService();
            var intent = service.Create(options);

            return new PaymentResultDto
            {
                Url = _returnURL + "/checkout?PublicKey=" + _publicKey + "&ClientSecret=" + intent.ClientSecret,
                ClientSecret = intent.ClientSecret
            };
        }
    }
}
