using Marketplace.Contracts.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Data.Models.Stocks
{
    public class InformationSubCategory : BaseEntity, IIdentifiable<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int InformationCategoryId { get; set; }
        [ForeignKey("InformationCategoryId")]
        public virtual InformationCategory InformationCategory { get; set; }

        object IIdentifiable.Id => Id;
    }
}
