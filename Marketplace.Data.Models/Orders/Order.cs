using Marketplace.Contracts.Models;
using Marketplace.Data.Models.Orders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Data.Models
{
    public class Order : BaseEntity, IIdentifiable<int>
    {
        public int Id { get; set; }
        public double Price { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        object IIdentifiable.Id => Id;
    }
}
