using Marketplace.Models.Dto.Orders;
using Marketplace.Models.Dtos.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Orders
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderAsync(int orderId);
        Task<OrderDto> DeleteOrderAsync(int orderId);
        Task<OrderDto> UpdateOrderAsync(int orderId, OrderDto orderDto);
        Task<OrderDto> CreateOrderAsync(IEnumerable<AddProductToOrderDto> orderDto);
        Task<double> GetAmountAsync(int orderId);
        Task<bool> IsOrderExistsAsync(int orderId);
        Task UpdatePriceAsync(int orderId);
    }
}
