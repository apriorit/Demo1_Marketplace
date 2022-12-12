using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Marketplace.Data.Models.Stocks;

namespace Marketplace.Data.EntityConfigurations
{
    public class ProductInfoConfiguration : IEntityTypeConfiguration<ProductInfo>
    {
        public void Configure(EntityTypeBuilder<ProductInfo> builder)
        {
            builder.Property(e => e.ProductId);
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.PathToImage);
            builder.Property(e => e.CreatedAt);
            builder.Property(e => e.CreatedBy);
            builder.Property(e => e.ModifiedAt);
            builder.Property(e => e.ModifiedBy);
            builder.Property(e => e.InformationSubCategoryId);

            builder.ToTable(name: "ProductInfos");
        }
    }
}
