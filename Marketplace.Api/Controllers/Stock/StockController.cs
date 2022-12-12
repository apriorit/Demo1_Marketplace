using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Contracts.Stocks;
using Marketplace.Models.Constants;
using Marketplace.Models.Dto.Stock;
using Marketplace.Models.Dtos.Orders;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Marketplace.Api.Controllers.Stock
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductShortInfo[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductShortInfo[]>> GetAllProducts()
        {
            var response = await _stockService.GetAllProductsAsync();
            return response.IsNullOrEmpty()
                ? NotFound()
                : Ok(response);
        }

        [HttpGet("products/{subCategoryId}")]
        [ProducesResponseType(typeof(ProductShortInfo[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductShortInfo[]>> GetProductsBySubCategoryId(
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int subCategoryId)
        {
            var response = await _stockService.GetProductsBySubCategoryIdAsync(subCategoryId);
            return response.IsNullOrEmpty()
                ? NotFound()
                : Ok(response);
        }

        [HttpGet("product/{id}")]
        [ProducesResponseType(typeof(ProductDetail[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDetail[]>> Get(
            [Required]
            [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
            int id)
        {
            var response = await _stockService.GetProductDetailsAsync(id);
            return response.IsNullOrEmpty()
                ? NotFound()
                : Ok(response);
        }

        [HttpPut("bookinginc")]
        public Task AddBooking(ProductCountDto productCount)
        {
            return _stockService.AddProductBookingAsync(productCount.Count, productCount.ProductId);
        }

        [HttpPut("bookingdec")]
        public Task RemoveBooking(ProductCountDto productCount)
        {
            return _stockService.RemoveProductBookingAsync(productCount.Count, productCount.ProductId);
        }

        [HttpPut("productinc")]
        public Task AddProductOnBalance(ProductCountDto productCount)
        {
            return _stockService.AddProductOnBalanceAsync(productCount.Count, productCount.ProductId);
        }
        [HttpPut("sellproduct")]
        public async Task SellProduct(ProductCountDto productCount)
        {
            await _stockService.SaleProductAsync(productCount.Count, productCount.ProductId);
        }
    }
}
