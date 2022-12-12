namespace Marketplace.Models.Constants
{
    public class StockExceptions
    {
        public static string ProductNotBookedCountException { private set { } get { return "This product wasn't booked in this count!"; } }
        public static string ProductNotBookedException { private set { } get { return "This product isn't booked!"; } }
        public static string StorageDoesNotContainProductCountException { private set { } get { return "The storage doesn't contain product in this count!"; } }
        public static string StockDoesNotContainProductException { private set { } get { return "Stock doesn't contain this product!"; } }
    }
}
