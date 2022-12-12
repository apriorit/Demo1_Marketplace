using Marketplace.Contracts;
using Marketplace.Contracts.Orders;
using Marketplace.Contracts.Stocks;
using Marketplace.Data.Infrastructure;
using Marketplace.Data.Models;
using Marketplace.Data.Models.Orders;
using Marketplace.Models.Dto.Orders;
using Marketplace.Models.Dtos.Orders;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services.Orders
{
    public class OrderProductService : DomainService<int, OrderProduct, OrderProductDto>, IOrderProductService
    {
        private readonly IRepository<OrderProduct> _orderProductRepo;
        private readonly IRepository<Order> _orderRepo;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IEntityConverter _entityConverter;

        public OrderProductService(
            IRepository<OrderProduct> orderProductRepo,
            IEntityConverter entityConverter,
            IRepository<Order> orderRepo,
            IProductService productService,
            IOrderService orderService)
            : base(orderProductRepo, entityConverter)
        {
            _orderProductRepo = orderProductRepo;
            _orderRepo = orderRepo;
            _productService = productService;
            _entityConverter = entityConverter;
            _orderService = orderService;
        }

        public async Task<OrderProductDto> AddProductToOrderAsync(int orderId, int productId, int quantity)
        {
            var existOrder = await _orderRepo.GetFirstOrDefaultAsync(o => o.Id == orderId);
            var existOrderProduct = _orderProductRepo.Get().FirstOrDefault(op => op.OrderId == orderId && op.ProductId == productId);
            var price = await _productService.GetProductPriceAsync(productId);

            if (existOrderProduct == null)
            {
                var orderProduct = new OrderProduct
                {
                    ProductId = productId,
                    Quantity = quantity,
                    OrderId = orderId
                };

                await _orderProductRepo.CreateAsync(orderProduct);

                return EntityConverter.ConvertTo<OrderProduct, OrderProductDto>(orderProduct);
            }

            existOrderProduct.Quantity += quantity;

            await _orderProductRepo.UpdateAsync(op => op.OrderId == orderId && op.ProductId == productId, existOrderProduct);

            existOrder.Price += price.CurrentPrice * quantity;

            return EntityConverter.ConvertTo<OrderProduct, OrderProductDto>(existOrderProduct);
        }

        public async Task<bool> DeleteProductFromOrderAsync(int orderProductId)
        {
            return await DeleteAsync(orderProductId) != null;
        }

        public Task<OrderProductDto> GetAsync(int orderProductId)
        {
            return FindAsync(orderProductId);
        }

        public async Task<OrderProductDto> UpdateOrderProductAsync(UpdateOrderProductDto updateOrderProduct)
        {
            var orderProduct = await _orderProductRepo.GetFirstOrDefaultAsync(op => op.Id == updateOrderProduct.Id);

            if (orderProduct == null)
            {
                return null;
            }

            _entityConverter.Merge(updateOrderProduct, orderProduct);
            var result = await UpdateAsync(updateOrderProduct.Id, _entityConverter.ConvertTo<OrderProduct, OrderProductDto>(orderProduct));
            await _orderService.UpdatePriceAsync(orderProduct.OrderId);
            return result;
        }
    }
}
