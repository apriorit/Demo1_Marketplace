using System;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models.Dto
{
    public class PriceDto
    {
        public int Id { get; set; }
        [Required]
        public double CurrentPrice { get; set; }
        public double OldPrice { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
