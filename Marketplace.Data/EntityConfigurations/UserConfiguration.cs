using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Marketplace.Data.Models;

namespace Marketplace.Data.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.Surname).HasMaxLength(50);
            builder.Property(e => e.Phone);
            builder.Property(e => e.Email);
            builder.Property(e => e.City);
            builder.Property(e => e.Street);
            builder.Property(e => e.ZipCode);
            builder.Property(e => e.Country);
            builder.Property(e => e.CreatedAt);
            builder.Property(e => e.CreatedBy);
            builder.Property(e => e.ModifiedAt);
            builder.Property(e => e.ModifiedBy);

            builder.ToTable(name: "Users");
        }
    }
}
