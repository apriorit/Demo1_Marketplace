using System;

namespace Marketplace.Contracts.Models
{
    public class BaseEntity : ICreatable, IModifiable, IDeletable
    {
        public bool IsActive { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
