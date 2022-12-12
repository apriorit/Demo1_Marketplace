using Marketplace.Contracts.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Data.Models.Stocks
{
    public class ProductInfo : BaseEntity, IIdentifiable<int>
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public string PathToImage { get; set; }

        public int InformationSubCategoryId { get; set; }
        [ForeignKey("InformationSubCategoryId")]
        public virtual InformationSubCategory InformationSubCategory { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        object IIdentifiable.Id => Id;
    }
}
