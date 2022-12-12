using System.Runtime.Serialization;

namespace Marketplace.Integrations.Payments.LiqPay.Models.Enums
{
    public enum LiqPayRequestActionPayment
    {
        [EnumMember(Value = "pay")]
        Pay = 1,
        [EnumMember(Value = "hold")]
        Hold = 2,
        [EnumMember(Value = "subscribe")]
        Subscribe = 3,
        [EnumMember(Value = "paydonate")]
        Paydonate = 4,
        [EnumMember(Value = "auth")]
        Auth = 6,
    }
}
