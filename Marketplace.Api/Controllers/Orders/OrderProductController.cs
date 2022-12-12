using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Contracts.Orders;
using Marketplace.Models.Constants;
using Marketplace.Models.Dto.Orders;
using Marketplace.Models.Dtos.Orders;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Marketplace.Api.Controllers.Orders
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductService _orderProductService;

        public OrderProductController(IOrderProductService productOrderService)
        {
            _orderProductService = productOrderService;
        }

        [HttpPost]
        public Task<OrderProductDto> AddProductToOrder(
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int orderId,
            AddProductToOrderDto product)
        {
            return _orderProductService.AddProductToOrderAsync(orderId, product.ProductId, product.Quantity);
        }

        [HttpGet]
        [ProducesResponseType(typeof(OrderProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderProductDto>> GetProductOrder(
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int orderProductId)
        {
            var response = await _orderProductService.GetAsync(orderProductId);
            return response == null
                ? NotFound()
                : Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(OrderProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderProductDto>> UpdateOrderProduct(
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int orderProductId, UpdateOrderProductDto orderProductDto)
        {
            if (orderProductId != orderProductDto.Id)
            {
                return BadRequest();
            }
            var response = await _orderProductService.UpdateOrderProductAsync(orderProductDto);
            return response == null
                ? NotFound()
                : Ok(response);
        }

        [HttpDelete("{productOrderId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteOrderProduct(
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int orderProductId)
        {
            var response = await _orderProductService.DeleteProductFromOrderAsync(orderProductId);
            return response
                ? Ok(response)
                : NotFound();

        }
    }
}
