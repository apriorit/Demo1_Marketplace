using Newtonsoft.Json;

namespace Marketplace.Integrations.Payments.LiqPay.Models
{
    public class LiqPayStatusData
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("version")]
        public int ApiVersion { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }
    }
}
