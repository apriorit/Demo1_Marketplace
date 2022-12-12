using Microsoft.EntityFrameworkCore;
using Marketplace.Data.Models;
using Marketplace.Data.Models.Orders;
using Marketplace.Data.Models.Payments;
using Marketplace.Data.Models.Stocks;
using Marketplace.Infrastructure.Extensions;
using Marketplace.Models.Enum.Payment;
using System;
using PaymentType = Marketplace.Data.Models.Payments.PaymentType;

namespace Marketplace.Data
{
    public static class DbSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var productCategories = new ProductCategory[]
            {
                new ProductCategory { Id = 1, Name = "Cars", IsActive = true },
                new ProductCategory { Id = 2, Name = "Displays", IsActive = true },
                new ProductCategory { Id = 3, Name = "Toys", IsActive = true }
            };

            var productSubCategories = new ProductSubCategory[]
            {
                new ProductSubCategory { Id = 1, Name = "Cars", IsActive = true, ProductCategoryId =1 },
                new ProductSubCategory { Id = 2, Name = "Displays", IsActive = true , ProductCategoryId =1},
                new ProductSubCategory { Id = 3, Name = "Toys", IsActive = true, ProductCategoryId =1 }
            };

            var products = new Product[]
            {
                new Product { Id = 1, Code = 1, Name = "BMW", IsActive = true, ProductSubCategoryId = 1},
                new Product { Id = 2, Code = 2, Name = "Toyota", IsActive = true, ProductSubCategoryId = 1},
                new Product { Id = 3, Code = 3, Name = "ZAZ", IsActive = true, ProductSubCategoryId = 1}
            };

            var orders = new Order[]
            {
                new Order { Id = 1, Price = 100.0, IsActive = true, CreatedAt = DateTime.UtcNow },
                new Order { Id = 2, Price = 300.0, IsActive = true, CreatedAt = DateTime.UtcNow  }
            };

            var orderProducts = new OrderProduct[]
            {
                new OrderProduct
                {
                    Id = 1,
                    ProductId = 1,
                    Quantity = 2,
                    OrderId = 1
                },
                new OrderProduct
                {
                    Id = 2,
                    ProductId = 2,
                    Quantity = 3,
                    OrderId = 1,
                },
                new OrderProduct
                {
                    Id = 3,
                    ProductId = 2,
                    Quantity = 1,
                    OrderId = 2
                },
                new OrderProduct
                {
                    Id = 4,
                    ProductId = 3,
                    Quantity = 1,
                    OrderId = 2
                }
            };

            var paymentTypes = new PaymentType[]
            {
                new PaymentType
                {
                    Id = (int)RequestActionType.Pay,
                    Name = EnumExtension.GetEnumMemberValue(RequestActionType.Pay)
                },
                new PaymentType
                {
                    Id = (int)RequestActionType.Hold,
                    Name = EnumExtension.GetEnumMemberValue(RequestActionType.Hold)
                },
            };

            var payments = new Payment[]
            {
                new Payment{ Id = 1, Amount = 100, IsActive = true, PaymentTypeId = 1, OrderId = 1, PaymentSystem = 1, Signature = String.Empty, Status = 1}
            };

            var informationCategory = new InformationCategory[]
            {
                new InformationCategory{Id = 1, Name = "Main", IsActive = true, CreatedAt= DateTime.UtcNow },
                new InformationCategory{Id = 2, Name = "Additional", IsActive = true, CreatedAt= DateTime.UtcNow }
            };

            var informationSubCategory = new InformationSubCategory[]
            {
                new InformationSubCategory{ Id = 1, Name = "PathToImage", IsActive = true, CreatedAt = DateTime.UtcNow, InformationCategoryId = 1},
                new InformationSubCategory{ Id = 2, Name = "PathToImage", IsActive = true, CreatedAt = DateTime.UtcNow, InformationCategoryId = 2},
                new InformationSubCategory{ Id = 3, Name = "Size", IsActive = true, CreatedAt = DateTime.UtcNow, InformationCategoryId = 2},
                new InformationSubCategory{ Id = 4, Name = "Type", IsActive = true, CreatedAt = DateTime.UtcNow, InformationCategoryId = 2}
            };

            var prices = new Price[]
            {
                new Price { Id = 1, CurrentPrice = 2000, OldPrice = 2010, ProductId = 1},
                new Price { Id = 2, CurrentPrice = 1000, OldPrice = 1010, ProductId = 2},
                new Price { Id = 3, CurrentPrice = 100, OldPrice = 101, ProductId = 3},
            };

            var productInfos = new ProductInfo[]
            {
                new ProductInfo{ Id = 1, Description = "Detail Info BMW", IsActive = true, ProductId = 1, PathToImage = "Path to Image BMW", InformationSubCategoryId = 1},
                new ProductInfo{ Id = 2, Description = "Detail Info Toyota", IsActive = true, ProductId = 2, PathToImage = "Path to Image Toyota", InformationSubCategoryId = 1},
                new ProductInfo{ Id = 3, Description = "Detail Info ZAZ", IsActive = true, ProductId = 3, PathToImage = "Path to Image ZAZ", InformationSubCategoryId = 1},
            };

            modelBuilder.Entity<ProductCategory>().HasData(productCategories);
            modelBuilder.Entity<ProductSubCategory>().HasData(productSubCategories);
            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Order>().HasData(orders);
            modelBuilder.Entity<OrderProduct>().HasData(orderProducts);
            modelBuilder.Entity<PaymentType>().HasData(paymentTypes);
            modelBuilder.Entity<Payment>().HasData(payments);
            modelBuilder.Entity<InformationCategory>().HasData(informationCategory);
            modelBuilder.Entity<InformationSubCategory>().HasData(informationSubCategory);
            modelBuilder.Entity<Price>().HasData(prices);
            modelBuilder.Entity<ProductInfo>().HasData(productInfos);
        }
    }
}
