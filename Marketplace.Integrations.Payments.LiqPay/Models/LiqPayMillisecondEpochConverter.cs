using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Marketplace.Integrations.Payments.LiqPay.Models
{
    public class LiqPayMillisecondEpochConverter : DateTimeConverterBase
    {
        private static readonly DateTime _epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override object? ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value == null
                ? null
                : _epoch.AddMilliseconds((long)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(((DateTime)value - _epoch).TotalMilliseconds.ToString());
        }
    }
}
