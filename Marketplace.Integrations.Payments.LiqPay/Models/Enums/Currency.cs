using System.Runtime.Serialization;

namespace Marketplace.Integrations.Payments.LiqPay.Enums
{
    public enum LiqPayCurrency
    {
        [EnumMember(Value = "USD")]
        USD = 1,
        [EnumMember(Value = "EUR")]
        EUR = 2,
        [EnumMember(Value = "UAH")]
        UAH = 3,
        [EnumMember(Value = "BYN")]
        BYN = 4,
        [EnumMember(Value = "KZT")]
        KZT = 5
    }
}
