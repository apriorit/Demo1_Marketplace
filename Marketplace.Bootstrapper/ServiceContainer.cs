using Autofac;
using AutoMapper;
using Marketplace.Contracts;
using Marketplace.Contracts.Orders;
using Marketplace.Contracts.Payments;
using Marketplace.Contracts.Stocks;
using Marketplace.Data;
using Marketplace.Data.Infrastructure;
using Marketplace.Data.Models;
using Marketplace.Infrastructure.Converters;
using Marketplace.Integrations.Payments.LiqPay.Services;
using Marketplace.Integrations.Payments.Stripe.Services;
using Marketplace.Models.Dto;
using Marketplace.Models.Dto.Orders;
using Marketplace.Models.Dto.Stock;
using Marketplace.Models.Dtos;
using Marketplace.Services;
using Marketplace.Services.Orders;
using Marketplace.Services.Payments;
using Marketplace.Services.Stock;
using System;

namespace Marketplace.Bootstrapper
{
    public class ServiceContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterMapper(builder);
            RegisterRepositories(builder);
            RegisterServises(builder);
        }

        private void RegisterMapper(ContainerBuilder builder)
        {
            var mapperConfigExpression = EntityConverter.GetDefaultMapperConfiguration(new string[]
            {
                "Marketplace.Models", "Marketplace.Integrations.Payments.LiqPay"
            });

            builder.Register(c => mapperConfigExpression).AsSelf().SingleInstance();
            builder.Register(ctx =>
            {
                var scope = ctx.Resolve<ILifetimeScope>();
                return new EntityConverter(ctx.Resolve<Action<IMapperConfigurationExpression>>(), scope.Resolve);
            }).As<IEntityConverter>().InstancePerLifetimeScope();
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>))
                .AsImplementedInterfaces()
                .WithParameter((p, ctx) => p.Name == "context", (p, ctx) => ctx.Resolve<ApplicationDbContext>())
                .InstancePerLifetimeScope();
        }

        private void RegisterServises(ContainerBuilder builder)
        {
            builder.RegisterType<StockService>()
                .As<IStockService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PaymentService>()
                .As<IPaymentService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderService>()
                .As<IOrderService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderProductService>()
                .As<IOrderProductService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<LiqPayPaymentService>()
                .As<IPaymentIntegrationSystem>()
                .Keyed<IPaymentIntegrationSystem>("LiqPay");

            builder.RegisterType<StripePaymentService>()
                .As<IPaymentIntegrationSystem>()
                .Keyed<IPaymentIntegrationSystem>("Stripe");

            builder.RegisterType<PaymentProvider>()
                .As<IPaymentProvider>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BookingService>()
                .As<IBookingService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StorageService>()
                .As<IStorageService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductService>()
                .As<IProductService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductCategoryService>()
                .As<IDomainService<int, ProductCategoryDto>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BookingService>()
                .As<IDomainService<int, BookingDto>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StorageService>()
                .As<IDomainService<int, StorageDto>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderService>()
                .As<IDomainService<int, OrderDto>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DomainService<int, ElasticEntity, ElasticDto>>()
                .As<IDomainService<int, ElasticDto>>()
                .InstancePerLifetimeScope();
        }
    }
}
