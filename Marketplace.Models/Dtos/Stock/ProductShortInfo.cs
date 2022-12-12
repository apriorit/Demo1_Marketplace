namespace Marketplace.Models.Dto.Stock
{
    public class ProductShortInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? CurrentPrice { get; set; }
        public double? OldPrice { get; set; }
        public string PathToImage { get; set; }
        public int? ProductSubCategoryId { get; set; }
    }
}
