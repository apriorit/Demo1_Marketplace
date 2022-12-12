using System;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models.Dto
{
    public class ProductInfoDto
    {
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public string PathToImage { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
        [Required]
        public int InformationSubCategoryId { get; set; }
    }
}
