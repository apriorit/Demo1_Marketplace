using Marketplace.Models.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models.Dto.Orders
{
    public class OrderDto
    {
        public List<OrderProductDto> OrderProducts { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.GREATER_THAN_ZERO)]
        public double Price { get; set; }
    }
}
