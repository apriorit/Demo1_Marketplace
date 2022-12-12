using Microsoft.EntityFrameworkCore;
using Marketplace.Data.Models;
using Marketplace.Data.Models.Orders;
using Marketplace.Data.Models.Payments;
using Marketplace.Data.Models.Stocks;

namespace Marketplace.Data
{
    public partial class ApplicationDbContext
    {
        public virtual DbSet<ElasticEntity> ElasticEntities { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductSubCategory> ProductSubCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<InformationCategory> InformationCategories { get; set; }
        public virtual DbSet<InformationSubCategory> InformationSubCategories { get; set; }
        public virtual DbSet<ProductInfo> ProductInfos { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<OrderProduct> OrderProduct { get; set; }
    }
}



