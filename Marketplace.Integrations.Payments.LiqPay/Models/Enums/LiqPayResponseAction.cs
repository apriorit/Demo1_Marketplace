using System.Runtime.Serialization;

namespace Marketplace.Integrations.Payments.LiqPay.Models.Enums
{
    public enum LiqPayResponseAction
    {
        [EnumMember(Value = "pay")]
        Pay = 1,
        [EnumMember(Value = "hold")]
        Hold = 2,
        [EnumMember(Value = "paysplit")]
        Paysplit = 3,
        [EnumMember(Value = "subscribe")]
        Subscribe = 4,
        [EnumMember(Value = "paydonate")]
        Paydonate = 5,
        [EnumMember(Value = "auth")]
        Auth = 6,
        [EnumMember(Value = "regular")]
        Regular = 7,
    }
}
