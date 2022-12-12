using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Marketplace.Data.Models.Stocks;

namespace Marketplace.Data.EntityConfigurations
{
    public class InformationCategoryConfiguration : IEntityTypeConfiguration<InformationCategory>
    {
        public void Configure(EntityTypeBuilder<InformationCategory> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.CreatedAt);
            builder.Property(e => e.CreatedBy);
            builder.Property(e => e.ModifiedAt);
            builder.Property(e => e.ModifiedBy);

            builder.ToTable(name: "InformationCategories");
        }
    }
}
