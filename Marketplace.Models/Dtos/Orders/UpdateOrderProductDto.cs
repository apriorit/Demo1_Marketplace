using Marketplace.Models.Constants;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models.Dtos.Orders
{
    public class UpdateOrderProductDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
        public int Quantity { get; set; }
    }
}
