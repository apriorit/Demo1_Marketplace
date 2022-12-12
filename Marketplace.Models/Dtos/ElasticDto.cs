using System;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models.Dtos
{
    public class ElasticDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
    }
}
