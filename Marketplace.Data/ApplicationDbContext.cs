using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Marketplace.Contracts.Models;
using Marketplace.Data.EntityConfigurations;
using Marketplace.Data.Models;
using Marketplace.Data.Models.Orders;
using Marketplace.Data.Models.Payments;
using Marketplace.Data.Models.Stocks;
using Marketplace.Models.Dto.Stock;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("Default");
                optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly("Marketplace.Data.Infrastructure"));
            }

            base.OnConfiguring(optionsBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetCreatableColumns();
            SetModifiableColumns();

            return base.SaveChangesAsync();
        }

        private void SetCreatableColumns()
        {
            var addedEntities = ChangeTracker
                    .Entries()
                    .Where(e => e.State == EntityState.Added && e.Entity is ICreatable);

            foreach (var entityEntry in addedEntities)
            {
                ((ICreatable)entityEntry.Entity).CreatedAt = DateTimeOffset.UtcNow;
            }
        }

        private void SetModifiableColumns()
        {
            var addedEntities = ChangeTracker
                    .Entries()
                    .Where(e => e.State == EntityState.Modified && e.Entity is IModifiable);

            foreach (var entityEntry in addedEntities)
            {
                ((IModifiable)entityEntry.Entity).ModifiedAt = DateTimeOffset.UtcNow;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            modelBuilder.ApplyConfiguration<Payment>(new PaymentEntityConfiguration());
            modelBuilder.ApplyConfiguration<PaymentType>(new PaymentTypeConfiguration());
            modelBuilder.ApplyConfiguration<Order>(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration<OrderProduct>(new OrderProductEntityConfiguration());
            modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
            modelBuilder.ApplyConfiguration<ProductInfo>(new ProductInfoConfiguration());
            modelBuilder.ApplyConfiguration<ProductCategory>(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration<ProductSubCategory>(new ProductSubCategoryConfiguration());
            modelBuilder.ApplyConfiguration<InformationCategory>(new InformationCategoryConfiguration());
            modelBuilder.ApplyConfiguration<InformationSubCategory>(new InformationSubCategoryConfiguration());
            modelBuilder.ApplyConfiguration<Storage>(new StorageConfiguration());
            modelBuilder.ApplyConfiguration<Booking>(new BookingConfiguration());
            modelBuilder.ApplyConfiguration<Price>(new PriceConfiguration());
            modelBuilder.ApplyConfiguration<ElasticEntity>(new ElasticEntityConfiguration());
            modelBuilder.ApplyConfiguration<ProductShortInfo>(new ProductShortInfoConfiguration());
            modelBuilder.ApplyConfiguration<ProductDetail>(new ProductDetailConfiguration());

            modelBuilder.Seed();
        }
    }
}
