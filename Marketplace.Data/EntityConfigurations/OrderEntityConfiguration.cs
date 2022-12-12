using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Marketplace.Data.Models;


namespace Marketplace.Data.EntityConfigurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(o => o.OrderProducts)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId);

            builder.ToTable(name: "Orders");
        }
    }
}
