using Newtonsoft.Json;

namespace Marketplace.Integrations.Payments.LiqPay.Models
{
    public class LiqPayData
    {
        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}
