using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models.Dto.Stock
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int Count { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
