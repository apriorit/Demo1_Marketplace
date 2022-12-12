using Marketplace.Contracts.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Data.Models.Payments
{
    public class Payment : BaseEntity, IIdentifiable<int>
    {
        public int Id { get; set; }
        public int PaymentSystem { get; set; }
        public int Status { get; set; }
        public double Amount { get; set; }
        public string Signature { get; set; }

        public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }


        public int PaymentTypeId { get; set; }
        [ForeignKey("PaymentTypeId")]
        public virtual PaymentType PaymentType { get; set; }


        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        object IIdentifiable.Id => Id;
    }
}
