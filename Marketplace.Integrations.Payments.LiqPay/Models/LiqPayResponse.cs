using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Marketplace.Integrations.Payments.LiqPay.Enums;
using Marketplace.Integrations.Payments.LiqPay.Models.Enums;

namespace Marketplace.Integrations.Payments.LiqPay.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class LiqPayResponse
    {
        [JsonProperty("acq_id")]
        public int AcqId { get; set; }

        [JsonProperty("action")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LiqPayResponseAction Action { get; set; }

        [JsonProperty("agent_commission")]
        public double AgentCommission { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("amount_bonus")]
        public double AmountBonus { get; set; }

        [JsonProperty("amount_credit")]
        public double AmountCredit { get; set; }

        [JsonProperty("amount_debit")]
        public double AmountDebit { get; set; }

        [JsonProperty("authcode_credit")]
        public string? AuthcodeCredit { get; set; }

        [JsonProperty("authcode_debit")]
        public string? AuthcodeDebit { get; set; }

        [JsonProperty("card_token")]
        public string? CardToken { get; set; }

        [JsonProperty("commission_credit")]
        public double CommissionCredit { get; set; }

        [JsonProperty("commission_debit")]
        public double CommissionDebit { get; set; }

        [JsonProperty("completion_date")]
        [JsonConverter(typeof(LiqPayMillisecondEpochConverter))]
        public DateTime? CompletionDate { get; set; }

        [JsonProperty("create_date")]
        [JsonConverter(typeof(LiqPayMillisecondEpochConverter))]
        public DateTime CreateDate { get; set; }

        [JsonProperty("currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LiqPayCurrency Currency { get; set; }

        [JsonProperty("currency_credit")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LiqPayCurrency CurrencyCredit { get; set; }

        [JsonProperty("currency_debit")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LiqPayCurrency CurrencyDebit { get; set; }

        [JsonProperty("customer")]
        public string? CustomerId { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("end_date")]
        [JsonConverter(typeof(LiqPayMillisecondEpochConverter))]
        public DateTime EndDate { get; set; }

        [JsonProperty("err_code")]
        public string? ErrorCode { get; set; }

        [JsonProperty("err_description")]
        public string? ErrorDescription { get; set; }

        [JsonProperty("info")]
        public string? Info { get; set; }

        [JsonProperty("ip")]
        public string? Ip { get; set; }

        [JsonProperty("is_3ds")]
        public bool Is3ds { get; set; }

        [JsonProperty("payment_order_id")]
        public string? PaymentOrderId { get; set; }

        [JsonProperty("mpi_eci")]
        public int MpiEci { get; set; }

        [JsonProperty("order_id")]
        public string? OrderId { get; set; }

        [JsonProperty("payment_id")]
        public long PaymentId { get; set; }

        [JsonProperty]
        public LiqPayResponseAction ResponseAction { get; set; }
    }
}
