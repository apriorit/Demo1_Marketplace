using Marketplace.Contracts.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Data.Models.Stocks
{
    public class Storage : BaseEntity, IIdentifiable<int>
    {
        public int Id { get; set; }
        public int Count { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        object IIdentifiable.Id => Id;
    }
}
