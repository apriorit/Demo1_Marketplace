using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Marketplace.Integrations.Payments.LiqPay.Enums;
using Marketplace.Integrations.Payments.LiqPay.Models.Enums;

namespace Marketplace.Integrations.Payments.LiqPay.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class LiqPayRequest
    {
        [JsonProperty("version")]
        public int ApiVersion { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("private_key")]
        public string PrivateKey { get; set; }

        [JsonProperty("action")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LiqPayRequestActionPayment Action { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LiqPayCurrency Currency { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("order_id")]
        public int OrderId { get; set; }

        [JsonProperty("expired_date")]
        [JsonConverter(typeof(LiqPayMillisecondEpochConverter))]
        public DateTime? ExpiredDate { get; set; }

        [JsonProperty("language")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LiqPayRequestLanguage? Language { get; set; }

        [JsonProperty("paytypes", ItemConverterType = typeof(StringEnumConverter))]
        public LiqPayPaymentType[] PayTypes { get; set; }

        [JsonProperty("result_url")]
        public string ResultUrl { get; set; }

        [JsonProperty("server_url")]
        public string ServerUrl { get; set; }


        [JsonProperty("verifycode")]
        private string? _verifycode;
        public bool? Verifycode
        {
            get => _verifycode == null ? null : _verifycode == "Y";
            init => _verifycode = value == true ? "Y" : null;
        }

        [JsonProperty("sender_address")]
        public string SenderAddress { get; set; }

        [JsonProperty("sender_city")]
        public string SenderCity { get; set; }

        [JsonProperty("sender_country_code")]
        public string SenderCountryCode { get; set; }

        [JsonProperty("sender_first_name")]
        public string SenderFirstName { get; set; }

        [JsonProperty("sender_last_name")]
        public string SenderLastName { get; set; }

        [JsonProperty("sender_postal_code")]
        public string SenderPostalCode { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("recurringbytoken")]
        private string? _recurringByToken;
        public bool? RecurringByToken
        {
            get => _recurringByToken == null ? null : _recurringByToken == "1";
            init => _recurringByToken = value == true ? "1" : null;
        }

        [JsonProperty("customer_user_id")]
        public string CustomerUserId { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("product_category")]
        public string ProductCategory { get; set; }

        [JsonProperty("product_description")]
        public string ProductDescription { get; set; }

        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        [JsonProperty("product_url")]
        public string ProductUrl { get; set; }

    }
}
