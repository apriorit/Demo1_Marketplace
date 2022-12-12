using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Marketplace.Models.Dto.Stock;

namespace Marketplace.Data.EntityConfigurations
{
    public class ProductShortInfoConfiguration : IEntityTypeConfiguration<ProductShortInfo>
    {
        public void Configure(EntityTypeBuilder<ProductShortInfo> builder)
        {
        }
    }
}
