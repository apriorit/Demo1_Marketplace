using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Marketplace.Data.Models.Stocks;

namespace Marketplace.Data.EntityConfigurations
{
    public class StorageConfiguration : IEntityTypeConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> builder)
        {
            builder.Property(e => e.ProductId);
            builder.Property(e => e.Count);

            builder.ToTable(name: "Storage");
        }
    }
}
