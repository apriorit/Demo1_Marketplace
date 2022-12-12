using System;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models.Dto
{
    public class StockDto
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public PriceDto Price { get; set; }
        [Required]
        [MaxLength(500)]
        public string Manufacturer { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
