namespace Marketplace.Integrations.Payments.Stripe.Common
{
    public static class StripeUtil
    {
        public static long ToLong(double amount)
        {
            return (long)(amount * 100);
        }

        public static double ToDouble(long amount)
        {
            return amount/100d;
        }
    }
}
