using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Marketplace.Data.Models;

namespace Marketplace.Data.EntityConfigurations
{
    public class ElasticEntityConfiguration : IEntityTypeConfiguration<ElasticEntity>
    {
        public void Configure(EntityTypeBuilder<ElasticEntity> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.Description).HasMaxLength(200);
            builder.Property(e => e.CreatedAt);
            builder.Property(e => e.ModifiedAt);

            builder.ToTable(name: "ElasticEntities");
        }
    }
}
