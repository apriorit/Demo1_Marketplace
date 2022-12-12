using Marketplace.Contracts.Models;

namespace Marketplace.Data.Models.Stocks
{
    public class ProductCategory : BaseEntity, IIdentifiable<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        object IIdentifiable.Id => Id;
    }
}

