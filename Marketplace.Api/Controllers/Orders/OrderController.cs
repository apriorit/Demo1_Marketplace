using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Contracts.Orders;
using Marketplace.Models.Constants;
using Marketplace.Models.Dto.Orders;
using Marketplace.Models.Dtos.Orders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Marketplace.Api.Controllers.Orders
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> GetOrder(
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int orderId)
        {
            var response = await _orderService.GetOrderAsync(orderId);
            return response == null
                ? NotFound()
                : Ok(response);
        }

        [HttpDelete("{orderId}")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> DeleteOrder(
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int orderId
            )
        {
            var response = await _orderService.DeleteOrderAsync(orderId);
            return response == null
                ? NotFound()
                : Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<OrderDto> CreateOrder(IEnumerable<AddProductToOrderDto> orderProducts)
        {
            return _orderService.CreateOrderAsync(orderProducts);
        }

        [HttpPut]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> UpdateOrder(
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int orderId,
            OrderDto order)
        {
            var response = await _orderService.UpdateOrderAsync(orderId, order);
            return response == null
                ? NotFound()
                : Ok(response);
        }
    }
}
