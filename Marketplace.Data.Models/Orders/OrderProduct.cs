using Marketplace.Contracts.Models;
using Marketplace.Data.Models.Stocks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Data.Models.Orders
{
    public class OrderProduct : BaseEntity, IIdentifiable<int>
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public int? StockId { get; set; }
        [ForeignKey("StockId")]
        public virtual Stock Stock { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        object IIdentifiable.Id => Id;
    }
}
