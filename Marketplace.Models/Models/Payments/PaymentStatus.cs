using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Marketplace.Models.Enum.Payment;
using System;

namespace Marketplace.Models.Models.Payments
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class PaymentStatus
    {
        [JsonProperty("action")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ResponseActionType Action { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("card_token")]
        public string CardToken { get; set; }

        [JsonProperty("completion_date")]
        public DateTime? CompletionDate { get; set; }

        [JsonProperty("create_date")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        [JsonProperty("customer")]
        public string CustomerId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty("err_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("err_description")]
        public string ErrorDescription { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("payment_order_id")]
        public string PaymentOrderId { get; set; }

        [JsonProperty("mpi_eci")]
        public int MpiEci { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("payment_id")]
        public long PaymentId { get; set; }
    }
}
