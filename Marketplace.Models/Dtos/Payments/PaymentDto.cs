using System;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models.Dto.Payments
{
    public class PaymentDto
    {
        public int Id { get; set; }
        [Required]
        public int PaymentTypeId { get; set; }
        [Required]
        public int OrderId { get; set; }
        public double Price { get; set; }
        public int? UserId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
