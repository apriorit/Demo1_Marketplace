using Marketplace.Contracts.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Data.Models.Stocks
{
    public class ProductSubCategory : BaseEntity, IIdentifiable<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProductCategoryId { get; set; }
        [ForeignKey("ProductCategoryId")]
        public virtual ProductCategory ProductCategory { get; set; }

        object IIdentifiable.Id => Id;
    }
}

