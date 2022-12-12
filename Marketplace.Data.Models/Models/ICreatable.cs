using System;

namespace Marketplace.Contracts.Models
{
    public interface ICreatable
    {
        DateTimeOffset CreatedAt { get; set; }
    }
}
