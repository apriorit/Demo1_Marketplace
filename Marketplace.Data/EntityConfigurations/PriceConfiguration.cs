using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Marketplace.Data.Models.Stocks;

namespace Marketplace.Data.EntityConfigurations
{
    public class PriceConfiguration : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.Property(e => e.ProductId);
            builder.Property(e => e.CurrentPrice);
            builder.Property(e => e.OldPrice);
            builder.Property(e => e.CreatedAt);
            builder.Property(e => e.CreatedBy);
            builder.Property(e => e.ModifiedAt);
            builder.Property(e => e.ModifiedBy);

            builder.ToTable(name: "Prices");
        }
    }
}
