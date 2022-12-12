using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Marketplace.Data.Models.Stocks;

namespace Marketplace.Data.EntityConfigurations
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.Property(e => e.ProductId);
            builder.Property(e => e.Quantity);
            builder.Property(e => e.Price);
            builder.Property(e => e.Manufacturer).HasMaxLength(500);
            builder.Property(e => e.CreatedAt);
            builder.Property(e => e.CreatedBy);
            builder.Property(e => e.ModifiedAt);
            builder.Property(e => e.ModifiedBy);

            builder.ToTable(name: "Stock");
        }
    }
}
