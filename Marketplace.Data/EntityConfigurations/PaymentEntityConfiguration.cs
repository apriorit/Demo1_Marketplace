using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Marketplace.Data.Models.Payments;

namespace Marketplace.Data.EntityConfigurations
{
    public class PaymentEntityConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(p => p.PaymentSystem).HasConversion<int>();
            builder.HasOne(p => p.PaymentType).WithMany().HasForeignKey(p => p.PaymentTypeId);

            builder.ToTable(name: "Payments");
        }
    }
}
