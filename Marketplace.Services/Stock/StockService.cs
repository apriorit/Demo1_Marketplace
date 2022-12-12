using Marketplace.Contracts;
using Marketplace.Contracts.Stocks;
using Marketplace.Data.Infrastructure;
using Marketplace.Data.Models.Stocks;
using Marketplace.Infrastructure.Exceptions;
using Marketplace.Models.Constants;
using Marketplace.Models.Dto;
using Marketplace.Models.Dto.Products;
using Marketplace.Models.Dto.Stock;
using System;
using System.Threading.Tasks;

namespace Marketplace.Services.Stock
{
    public class StockService : DomainService<int, Product, ProductDto>, IStockService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IBookingService _bookingService;
        private readonly IStorageService _storageService;

        public StockService(IRepository<Product> productRepository, IBookingService bookingService,
            IStorageService storageService, IEntityConverter entityConverter)
            : base(productRepository, entityConverter)
        {
            _productRepository = productRepository;
            _bookingService = bookingService;
            _storageService = storageService;
        }

        public Task<ProductShortInfo[]> GetAllProductsAsync()
        {
            return _productRepository.ExecuteStoreProcedureAsync<ProductShortInfo>("EXEC [dbo].[usp_GetProductShortInfos]");
        }

        public Task<ProductShortInfo[]> GetProductsBySubCategoryIdAsync(int subCategoryId)
        {
            return _productRepository.ExecuteStoreProcedureAsync<ProductShortInfo>($"EXEC [dbo].[usp_GetProductShortInfosBySubcategory] @SubCategoryId = {subCategoryId}");
        }

        public Task<ProductDetail[]> GetProductDetailsAsync(int id)
        {
            return _productRepository.ExecuteStoreProcedureAsync<ProductDetail>($"EXEC [dbo].[usp_GetProductDetailById] @ProductId = {id}");
        }

        private async Task IncreaseBookingAsync(int count, int productId)
        {
            var currentProductId = productId;

            var productInStorageInfo = await _storageService.FindStorageByProductIdAsync(currentProductId);

            if (productInStorageInfo == null)
            {
                throw new NotFoundException(StockExceptions.StockDoesNotContainProductException);
            }

            var currentStockCount = productInStorageInfo.Count;

            var booking = await _bookingService.GetBookingByProductIdAsync(currentProductId);
            if (booking != null)
            {
                var newCount = booking.Count + count;
                var currentBookingId = booking.Id;
                if (newCount <= currentStockCount)
                {
                    var newBooking = new BookingDto
                    {
                        ProductId = currentProductId,
                        Count = newCount
                    };

                    await _bookingService.UpdateBookingAsync(currentBookingId, newBooking);
                }
                else
                {
                    throw new NotFoundException(StockExceptions.StorageDoesNotContainProductCountException);
                }
            }
            else
            {
                if (count <= currentStockCount)
                {
                    var newBooking = new BookingDto { ProductId = currentProductId, Count = count };
                    await _bookingService.CreateBookingAsync(newBooking);
                }
                else
                {
                    throw new NotFoundException(StockExceptions.StorageDoesNotContainProductCountException);
                }
            }
        }

        public Task AddProductBookingAsync(int count, int productId)
        {
            return _repository.WithTransactionAsync(() => IncreaseBookingAsync(count, productId));
        }

        public async Task<BookingDto> RemoveProductBookingAsync(int count, int productId)
        {
            var currentProductId = productId;

            var booking = await _bookingService.GetBookingByProductIdAsync(currentProductId);
            if (booking != null)
            {
                var newCount = booking.Count - count;
                var currentBookingId = booking.Id;
                if (newCount >= 0)
                {
                    var newBooking = new BookingDto
                    {
                        ProductId = currentProductId,
                        Count = newCount
                    };
                    return await _bookingService.UpdateBookingAsync(currentBookingId, newBooking);
                }
                else
                {
                    throw new Exception(StockExceptions.ProductNotBookedCountException);
                }
            }
            else
            {
                throw new Exception(StockExceptions.ProductNotBookedException);
            }
        }

        public async Task ChangeProductBalanceAsync(int count, int productId)
        {
            var currentProductId = productId;

            var productStorage = await _storageService.FindStorageByProductIdAsync(currentProductId);

            if (productStorage != null)
            {
                var newCount = productStorage.Count + count;
                var currentProductStorageId = productStorage.Id;
                if (newCount >= 0)
                {
                    var newProductStorage = new StorageDto
                    {
                        ProductId = currentProductId,
                        Count = newCount
                    };
                    await _storageService.UpdateStorageAsync(currentProductStorageId, newProductStorage);
                }
                else
                {
                    throw new Exception(StockExceptions.StorageDoesNotContainProductCountException);
                }
            }
            else
            {
                if (count < 0)
                {
                    throw new Exception(StockExceptions.StorageDoesNotContainProductCountException);
                }
                var newStorage = new StorageDto { ProductId = currentProductId, Count = count };
                await _storageService.CreateStorageAsync(newStorage);
            }
        }

        public Task AddProductOnBalanceAsync(int count, int productId)
        {
            return ChangeProductBalanceAsync(count, productId);
        }

        public Task RemoveProductFromBalanceAsync(int count, int productId)
        {
            return ChangeProductBalanceAsync(-count, productId);
        }

        public Task SaleProductAsync(int count, int productId)
        {
            return _repository.WithTransactionAsync(() => RemoveProductAfterSale(count, productId));
        }

        private async Task RemoveProductAfterSale(int count, int productId)
        {
            await RemoveProductBookingAsync(count, productId);
            await RemoveProductFromBalanceAsync(count, productId);
        }
    }
}
