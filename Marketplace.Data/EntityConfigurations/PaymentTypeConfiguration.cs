using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Marketplace.Data.Models.Payments;

namespace Marketplace.Data.EntityConfigurations
{
    internal class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.CreatedAt);
            builder.Property(e => e.CreatedBy);
            builder.Property(e => e.ModifiedAt);
            builder.Property(e => e.ModifiedBy);

            builder.ToTable(name: "PaymentTypes");
        }
    }
}
