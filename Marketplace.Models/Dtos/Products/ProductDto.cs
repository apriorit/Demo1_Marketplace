using Marketplace.Models.Dto.Stock;

namespace Marketplace.Models.Dto.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public ProductSubCategoryDto ProductSubCategory { get; set; }
    }
}
