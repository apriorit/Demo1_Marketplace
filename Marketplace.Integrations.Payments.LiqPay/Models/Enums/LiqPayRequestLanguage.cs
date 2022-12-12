using System.Runtime.Serialization;

namespace Marketplace.Integrations.Payments.LiqPay.Models.Enums
{
    public enum LiqPayRequestLanguage
    {
        [EnumMember(Value = "uk")]
        UK = 1,
        [EnumMember(Value = "en")]
        EN = 2,
        [EnumMember(Value = "ru")]
        RU = 3,
    }
}
