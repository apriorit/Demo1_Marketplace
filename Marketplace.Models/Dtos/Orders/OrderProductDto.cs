using Marketplace.Models.Constants;
using Marketplace.Models.Dto.Products;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models.Dto.Orders
{
    public class OrderProductDto
    {
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
        public int? StockId { get; set; }
        public StockDto Stock { get; set; }
        public int? OrderId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
        public int Quantity { get; set; }
    }
}
