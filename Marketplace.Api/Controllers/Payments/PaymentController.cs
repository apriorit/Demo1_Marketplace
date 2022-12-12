using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Contracts.Payments;
using Marketplace.Models.Constants;
using Marketplace.Models.Dtos.Payments;
using Marketplace.Models.Enum.Payment;
using Marketplace.Models.Models.Payments;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Marketplace.Api.Controllers.Payment
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("request")]
        [ProducesResponseType(typeof(PaymentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentResult>> PaymentRequest(
            [Required] RequestActionType action,
            [Required] Currency currency,
            [Required] PaymentSystemType paymentSystemType,
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int orderId,
            [Required] string description)
        {
            var response = await _paymentService.RequestPaymentAsync(
                new CreatePaymentDto
                {
                    Action = action,
                    Currency = currency,
                    OrderId = orderId,
                    Description = description,
                    PaymentSystemType = paymentSystemType
                });
            return response == null
                ? NotFound()
                : Ok(response);
        }

        [HttpGet("status")]
        [ProducesResponseType(typeof(PaymentStatus), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentStatus>> GetStatus(
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int orderId)
        {
            var response = await _paymentService.GetPaymentStatusAsync(orderId);
            return response == null
                ? NotFound()
                : Ok(response);
        }

        [HttpPost("liqpay_response")]
        [ProducesResponseType(typeof(PaymentStatus), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentStatusType>> PaymentResponse(string data, string signature)
        {
            return await _paymentService.ReceiveResponseAsync(data, signature);
        }
    }
}
