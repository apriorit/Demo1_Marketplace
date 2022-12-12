using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models.Dto
{
    public class StorageDto
    {
        public int Id { get; set; }
        public int Count { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
