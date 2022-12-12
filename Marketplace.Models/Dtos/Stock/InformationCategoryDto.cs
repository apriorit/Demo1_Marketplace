using System;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models.Dto
{
    public class InformationCategoryDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string InformationCategoryName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
