using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Marketplace.Contracts;
using Marketplace.Contracts.Payments;
using Marketplace.Infrastructure.Exceptions;
using Marketplace.Integrations.Payments.LiqPay.Common;
using Marketplace.Integrations.Payments.LiqPay.Enums;
using Marketplace.Integrations.Payments.LiqPay.Models;
using Marketplace.Integrations.Payments.LiqPay.Models.Enums;
using Marketplace.Models.Dtos.Payments;
using Marketplace.Models.Enum.Payment;
using Marketplace.Models.Models.Payments;
using System.Net;
using System.Text;

namespace Marketplace.Integrations.Payments.LiqPay.Services
{
    public class LiqPayPaymentService : IPaymentIntegrationSystem
    {
        private readonly string _publicKey;
        private readonly string _privateKey;

        private const string _requestUrl = "request";

        private readonly IEntityConverter _entityConverter;

        public LiqPayPaymentService(IConfiguration config, IEntityConverter entityConverter)
        {
            _publicKey = config.GetValue<string>("LiqPay:PublicKey");
            _privateKey = config.GetValue<string>("LiqPay:PrivateKey");

            if (string.IsNullOrEmpty(_publicKey) || string.IsNullOrEmpty(_privateKey))
            {
                throw new ArgumentException("LiqPay credentials are not valid");
            }

            _entityConverter = entityConverter;
        }

        public PaymentStatus DecodePaymentResponse(string data)
        {
            var responseJson = data.DecodeBase64();

            if (responseJson == null)
            {
                throw new IncorrectResponseException("Received payment response is incorrect");
            }

            return JsonConvert.DeserializeObject<PaymentStatus>(responseJson) ?? throw new IncorrectResponseException("Received payment response is incorrect");
        }

        public async Task<PaymentStatus> GetPaymentStatusAsync(int orderId)
        {
            using var client = new HttpClient();
            var data = new LiqPayStatusData
            {
                Action = LiqPayConsts.LiqPayStatusAction,
                ApiVersion = LiqPayConsts.ApiVersion,
                PublicKey = _publicKey,
                OrderId = orderId.ToString(),
            };

            var liqPayData = GetData(data);
            var dataParams = new Dictionary<string, string>
            {
                { "data", liqPayData.Data },
                { "signature", liqPayData.Signature }
            };

            var response = await client.PostAsync(LiqPayConsts.LiqPayApiUrl + _requestUrl, new StringContent(CreateUrlParametrs(dataParams)));
            var result = await response.Content.ReadAsStringAsync();
            var paymentStatus = new PaymentStatus();

            var liqPayStatus = JsonConvert.DeserializeObject<LiqPayResponse>(result);

            if (liqPayStatus != null)
            {
                _entityConverter.AssignTo<LiqPayResponse, PaymentStatus>(liqPayStatus, ref paymentStatus);
            }

            return paymentStatus;
        }

        private LiqPayRequestActionPayment GetLiqPayAction(RequestActionType action)
        {
            return action switch
            {
                RequestActionType.Pay => LiqPayRequestActionPayment.Pay,
                RequestActionType.Hold => LiqPayRequestActionPayment.Hold,
                RequestActionType.Subscribe => LiqPayRequestActionPayment.Subscribe,
                RequestActionType.Paydonate => LiqPayRequestActionPayment.Paydonate,
                RequestActionType.Auth => LiqPayRequestActionPayment.Auth,
                _ => throw new NotImplementedException()
            };
        }

        private LiqPayCurrency GetLiqPayCurrency(Currency currency)
        {
            return currency switch
            {
                Currency.USD => LiqPayCurrency.USD,
                Currency.EUR => LiqPayCurrency.EUR,
                Currency.UAH => LiqPayCurrency.UAH,
                Currency.BYN => LiqPayCurrency.BYN,
                Currency.KZT => LiqPayCurrency.KZT,
                _ => throw new NotImplementedException(),
            };
        }

        private LiqPayRequestLanguage GetLiqPayLanguage(Language language)
        {
            return language switch
            {
                Language.UK => LiqPayRequestLanguage.UK,
                Language.EN => LiqPayRequestLanguage.EN,
                Language.RU => LiqPayRequestLanguage.RU,
                _ => throw new NotImplementedException(),
            };
        }

        public PaymentResultDto RequestPayment(PaymentInfoDto paymentInfo)
        {
            var liqPayRequest = new LiqPayRequest
            {
                Action = GetLiqPayAction(paymentInfo.Action),
                Amount = paymentInfo.Amount,
                Currency = GetLiqPayCurrency(paymentInfo.Currency),
                Description = paymentInfo.Description,
                Language = GetLiqPayLanguage(paymentInfo.Language),
                OrderId = paymentInfo.OrderId
            };

            ConfigureRequest(liqPayRequest);

            var liqPayData = GetData(liqPayRequest);

            return new PaymentResultDto
            {
                Url = LiqPayConsts.LiqPayApiCheckoutUrl,
                Data = liqPayData.Data,
                Signature = liqPayData.Signature
            };
        }

        private void ConfigureRequest(LiqPayRequest request)
        {
            request.ApiVersion = LiqPayConsts.ApiVersion;
            request.PublicKey = _publicKey;
            request.PrivateKey = _privateKey;
        }

        private string CreateUrlParametrs(Dictionary<string, string> parametrs)
        {
            var parameters = new List<string>();
            foreach (var item in parametrs)
            {
                var queryValue = WebUtility.HtmlEncode(item.Value);
                var bytes = Encoding.Default.GetBytes(queryValue);
                var utf8QueryValue = Encoding.UTF8.GetString(bytes);

                parameters.Add($"{item.Key}={utf8QueryValue}");
            }

            return string.Join("&", parameters);
        }

        private LiqPayData GetData(object requestData)
        {
            var liqPayData = new LiqPayData();
            liqPayData.Data = JsonConvert.SerializeObject(requestData).ToBase64String() ?? throw new IncorrectResponseException("Request data is incorrect");
            liqPayData.Signature = (_privateKey + liqPayData.Data + _privateKey)
                .SHA1Hash()
                .ToBase64String();

            return liqPayData;
        }
    }
}
