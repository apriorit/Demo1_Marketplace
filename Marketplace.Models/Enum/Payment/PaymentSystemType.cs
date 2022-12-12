using System.Runtime.Serialization;

namespace Marketplace.Models.Enum.Payment
{
    public enum PaymentSystemType
    {
        [EnumMember(Value = "LiqPay")]
        LiqPay = 1,
        [EnumMember(Value = "Stripe")]
        Stripe = 2,
    }
}
