using Marketplace.Contracts.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Data.Models.Stocks
{
    public class Product : BaseEntity, IIdentifiable<int>
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }

        public int? ProductSubCategoryId { get; set; }
        [ForeignKey("ProductSubCategoryId")]
        public virtual ProductSubCategory ProductSubCategory { get; set; }

        object IIdentifiable.Id => Id;
    }
}
