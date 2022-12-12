using Marketplace.Models.Dto.Stock;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Stocks
{
    public interface IStockService
    {
        public Task<ProductShortInfo[]> GetAllProductsAsync();
        public Task<ProductShortInfo[]> GetProductsBySubCategoryIdAsync(int subCategoryId);
        public Task<ProductDetail[]> GetProductDetailsAsync(int id);
        public Task AddProductBookingAsync(int count, int productId);
        public Task<BookingDto> RemoveProductBookingAsync(int count, int productId);
        public Task AddProductOnBalanceAsync(int count, int productId);
        public Task SaleProductAsync(int count, int productId);
    }
}
