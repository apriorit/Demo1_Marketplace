using Marketplace.Data.Models.Stocks;
using System.Collections.Generic;

namespace Marketplace.Models.Dto.Stock
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public double CurrentPrice { get; set; }
        public double OldPrice { get; set; }
        public string Description { get; set; }
        public string PathToImage { get; set; }
        public IEnumerable<ProductInfo> ProductInfoList { get; set; }
    }
}
