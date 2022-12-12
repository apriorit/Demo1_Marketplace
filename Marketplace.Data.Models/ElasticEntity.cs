using Marketplace.Contracts.Models;

namespace Marketplace.Data.Models
{
    public class ElasticEntity : BaseEntity, IIdentifiable<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        object IIdentifiable.Id => Id;
    }
}
