using Marketplace.Integrations.Payments.LiqPay.Models.Enums;

namespace Marketplace.Integrations.Payments.LiqPay.Models
{
    public static class LiqPayConsts
    {
        public const int ApiVersion = 3;
        public const string LiqPayApiUrl = "https://www.liqpay.ua/api/";
        public const string LiqPayApiCheckoutUrl = "https://www.liqpay.ua/api/3/checkout";
        public const LiqPayRequestLanguage DefaultLanguage = LiqPayRequestLanguage.UK;
        public const string LiqPayStatusAction = "status";
    }
}
