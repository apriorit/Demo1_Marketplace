using Marketplace.Contracts.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Data.Models.Stocks
{
    public class Price : BaseEntity, IIdentifiable<int>
    {
        public int Id { get; set; }
        public double CurrentPrice { get; set; }
        public double OldPrice { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        object IIdentifiable.Id => Id;
    }
}
