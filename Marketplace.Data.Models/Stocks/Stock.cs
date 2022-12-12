using Marketplace.Contracts.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Data.Models.Stocks
{
    public class Stock : BaseEntity, IIdentifiable<int>
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Manufacturer { get; set; }

        public int PriceId { get; set; }
        [ForeignKey("PriceId")]
        public virtual Price Price { get; set; }

        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        object IIdentifiable.Id => Id;
    }
}
