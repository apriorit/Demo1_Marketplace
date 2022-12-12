using Marketplace.Models.Dto;
using Marketplace.Models.Dtos.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Stocks
{
    public interface IProductService
    {
        Task<IEnumerable<PriceDto>> GetProductsPrices(IEnumerable<int> productIds);
        Task<PriceDto> GetProductPriceAsync(int productId);
        Task<double> GetProductsAmountAsync(IEnumerable<AddProductToOrderDto> orderProducts);
    }
}
