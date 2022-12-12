using System.Runtime.Serialization;

namespace Marketplace.Integrations.Payments.LiqPay.Models.Enums
{
    public enum LiqPayPaymentType
    {
        [EnumMember(Value = "apay")]
        Apay = 1,
        [EnumMember(Value = "gpay")]
        Gpay = 2,
        [EnumMember(Value = "card")]
        Card = 3,
        [EnumMember(Value = "liqpay")]
        LiqPay = 4,
        [EnumMember(Value = "privat24")]
        Privat24 = 5,
        [EnumMember(Value = "masterpass")]
        Masterpass = 6,
        [EnumMember(Value = "moment_part")]
        MomentPart = 7,
        [EnumMember(Value = "cash")]
        Cash = 8,
        [EnumMember(Value = "invoice")]
        Invoice = 9,
        [EnumMember(Value = "qr")]
        Qr = 10
    }
}
