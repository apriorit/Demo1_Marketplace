using Marketplace.Models.Constants;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models.Dtos.Orders
{
    public class AddProductToOrderDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
        public int ProductId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
        public int Quantity { get; set; }
    }
}
