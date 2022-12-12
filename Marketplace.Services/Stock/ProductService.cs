using Microsoft.EntityFrameworkCore;
using Marketplace.Contracts;
using Marketplace.Contracts.Stocks;
using Marketplace.Data.Infrastructure;
using Marketplace.Data.Models.Orders;
using Marketplace.Data.Models.Stocks;
using Marketplace.Models.Dto;
using Marketplace.Models.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services.Stock
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Price> _priceRepo;
        private readonly IRepository<OrderProduct> _orderProductRepo;
        private readonly IEntityConverter _entityConverter;

        public ProductService(IRepository<Price> priceRepo, IEntityConverter entityConverter, IRepository<OrderProduct> orderProductRepo)
        {
            _priceRepo = priceRepo;
            _entityConverter = entityConverter;
            _orderProductRepo = orderProductRepo;
        }

        public async Task<PriceDto> GetProductPriceAsync(int productId)
        {
            var price = await _priceRepo.GetFirstOrDefaultAsync(p => p.ProductId == productId);

            return _entityConverter.ConvertTo<Price, PriceDto>(price);
        }

        public async Task<IEnumerable<PriceDto>> GetProductsPrices(IEnumerable<int> productIds)
        {
            var productPrice = await _priceRepo.GetAllAsync(p => productIds.Contains(p.ProductId));

            return _entityConverter.ConvertTo<IEnumerable<Price>, IEnumerable<PriceDto>>(productPrice);
        }

        public Task<double> GetProductsAmountAsync(IEnumerable<AddProductToOrderDto> orderProducts)
        {
            var prices = _priceRepo.Get().Where(p => orderProducts.Select(op => op.ProductId).Contains(p.ProductId));
            var priceQuery =
                from op in orderProducts
                join p in prices on op.ProductId equals p.ProductId
                select new
                {
                    op.Quantity,
                    p.CurrentPrice
                };

            return Task.FromResult(priceQuery.Sum(pq => pq.Quantity * pq.CurrentPrice));
        }


    }
}
