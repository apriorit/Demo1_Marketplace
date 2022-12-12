using Marketplace.Contracts.Models;

namespace Marketplace.Data.Models.Payments
{
    public class PaymentType : BaseEntity, IIdentifiable<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        object IIdentifiable.Id => Id;
    }
}
