using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Marketplace.Data.Models.Stocks;

namespace Marketplace.Data.EntityConfigurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(e => e.ProductId);
            builder.Property(e => e.Count);

            builder.ToTable(name: "Bookings");
        }
    }
}
