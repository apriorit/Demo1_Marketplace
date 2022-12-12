using System;

namespace Marketplace.Contracts.Models
{
    public interface IModifiable
    {
        DateTimeOffset? ModifiedAt { get; set; }
    }
}
