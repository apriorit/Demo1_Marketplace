using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Marketplace.Data.Models.Orders;

namespace Marketplace.Data.EntityConfigurations
{
    public class OrderProductEntityConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasOne(o => o.Order).WithMany(o => o.OrderProducts);
            builder.HasOne(op => op.Product).WithMany().HasForeignKey(op => op.ProductId);

            builder.ToTable(name: "OrderProducts");
        }
    }
}
