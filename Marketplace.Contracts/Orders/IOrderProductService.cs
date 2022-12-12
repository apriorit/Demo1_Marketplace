using Marketplace.Models.Dto.Orders;
using Marketplace.Models.Dtos.Orders;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Orders
{
    public interface IOrderProductService
    {
        Task<OrderProductDto> AddProductToOrderAsync(int orderId, int productId, int quantity);
        Task<bool> DeleteProductFromOrderAsync(int orderProductId);
        Task<OrderProductDto> UpdateOrderProductAsync(UpdateOrderProductDto updateOrderProduct);
        Task<OrderProductDto> GetAsync(int orderProductId);
    }
}
