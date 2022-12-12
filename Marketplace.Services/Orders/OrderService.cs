using Microsoft.EntityFrameworkCore;
using Marketplace.Contracts;
using Marketplace.Contracts.Orders;
using Marketplace.Contracts.Stocks;
using Marketplace.Data.Infrastructure;
using Marketplace.Data.Models;
using Marketplace.Data.Models.Orders;
using Marketplace.Data.Models.Stocks;
using Marketplace.Infrastructure.Exceptions;
using Marketplace.Models.Dto.Orders;
using Marketplace.Models.Dtos.Orders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services.Orders
{
    public class OrderService : DomainService<int, Order, OrderDto>, IOrderService
    {
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<OrderProduct> _orderProductRepo;
        private readonly IRepository<Price> _priceRepo;
        private readonly IProductService _productService;

        public OrderService(IRepository<Order> orderRepo, IEntityConverter entityConverter, IProductService productService, IRepository<Price> priceRepo, IRepository<OrderProduct> orderProductRepo)
            : base(orderRepo, entityConverter)
        {
            _orderRepo = orderRepo;
            _productService = productService;
            _priceRepo = priceRepo;
            _orderProductRepo = orderProductRepo;
        }

        public async Task<OrderDto> CreateOrderAsync(IEnumerable<AddProductToOrderDto> orderProducts)
        {
            var order = new OrderDto
            {
                OrderProducts = orderProducts.Select(op => new OrderProductDto
                {
                    ProductId = op.ProductId,
                    Quantity = op.Quantity
                }).ToList(),
                Price = await _productService.GetProductsAmountAsync(orderProducts)
            };

            return await CreateAsync(order);
        }

        public Task<OrderDto> DeleteOrderAsync(int orderId)
        {
            return base.DeleteAsync(orderId);
        }

        public async Task<double> GetAmountAsync(int orderId)
        {
            var order = await _orderRepo.GetFirstOrDefaultAsync(predicate: o => o.Id == orderId);
            return order == null
                ? throw new NotFoundException("order")
                : order.Price;
        }

        public async Task<OrderDto> GetOrderAsync(int orderId)
        {
            var order = await _orderRepo.GetFirstOrDefaultAsync(
                predicate: o => o.Id == orderId,
                include: query => query
                    .Include(o => o.OrderProducts)
                        .ThenInclude(op => op.Product)
                    .Include(o => o.OrderProducts)
                        .ThenInclude(op => op.Stock));

            return order == null ? default : EntityConverter.ConvertTo<Order, OrderDto>(order);
        }

        public Task<bool> IsOrderExistsAsync(int orderId)
        {
            return _orderRepo.Get().AnyAsync(o => o.Id == orderId);
        }

        public Task<OrderDto> UpdateOrderAsync(int orderId, OrderDto orderDto)
        {
            return UpdateAsync(orderId, orderDto);
        }

        public async Task UpdatePriceAsync(int orderId)
        {
            var order = await FindAsync(orderId);

            var priceQuery = from op in _orderProductRepo.Get()
                             join p in _priceRepo.Get() on op.ProductId equals p.ProductId
                             where op.OrderId == orderId
                             select new
                             {
                                 op.Quantity,
                                 p.CurrentPrice
                             };

            order.Price = priceQuery.Sum(pq => pq.Quantity * pq.CurrentPrice);

            await UpdateAsync(orderId, order);
        }
    }
}
