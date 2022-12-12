using Moq;
using Marketplace.Contracts;
using Marketplace.Contracts.Stocks;
using Marketplace.Data.Infrastructure;
using Marketplace.Data.Models.Stocks;
using Marketplace.Models.Constants;
using Marketplace.Models.Dto.Stock;
using Marketplace.Services.Stock;
using Xunit;

namespace Tests
{
    public class MarketplaceTests
    {
        [Fact]
        public async Task RemoveBookingProductToExisingBooking_Successful()
        {
            // Arrange
            var entityConverterMock = new Mock<IEntityConverter>();

            var currentId = 1;
            var currentProductId = 1;
            var newProductBookingCount = 10;
            var productBookingCount = 20;

            var bookingDtoElement = new BookingDto { Id = currentId, ProductId = currentProductId, Count = productBookingCount };

            var bookingServiceMock = new Mock<BookingService>(new Mock<IRepository<Booking>>().Object, entityConverterMock.Object);
            bookingServiceMock
                .Setup(bsm => bsm
                    .GetBookingByProductIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(bookingDtoElement);
            bookingServiceMock
                .Setup(bsm => bsm
                    .UpdateBookingAsync(It.Is<int>(i => i == bookingDtoElement.ProductId),
                        It.IsAny<BookingDto>()))
                    .ReturnsAsync(new BookingDto
                    {
                        Id = currentId,
                        ProductId = currentProductId,
                        Count = newProductBookingCount
                    });


            StockService _stockService = new StockService(new Mock<IRepository<Product>>().Object,
               bookingServiceMock.Object, new Mock<IStorageService>().Object, entityConverterMock.Object);

            // Act   
            var result = await _stockService.RemoveProductBookingAsync(productBookingCount, currentProductId);

            // Assert

            Assert.Equal(newProductBookingCount, result.Count);
        }

        [Fact]
        public async Task RemoveBookingProductBoundValue_Successful()
        {
            // Arrange
            var entityConverterMock = new Mock<IEntityConverter>();

            var currentId = 1;
            var currentProductId = 1;
            var productBookingCount = 20;

            var bookingDtoElement = new BookingDto { Id = currentId, ProductId = currentProductId, Count = productBookingCount };

            var bookingServiceMock = new Mock<BookingService>(new Mock<IRepository<Booking>>().Object, entityConverterMock.Object);
            bookingServiceMock
                .Setup(bsm => bsm
                    .GetBookingByProductIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(bookingDtoElement);
            bookingServiceMock
                .Setup(bsm => bsm
                    .UpdateBookingAsync(It.Is<int>(i => i == bookingDtoElement.ProductId),
                        It.IsAny<BookingDto>()))
                    .ReturnsAsync(new BookingDto
                    {
                        Id = currentId,
                        ProductId = currentProductId,
                        Count = 0
                    });


            StockService _stockService = new StockService(new Mock<IRepository<Product>>().Object,
               bookingServiceMock.Object, new Mock<IStorageService>().Object, entityConverterMock.Object);

            // Act   
            var result = await _stockService.RemoveProductBookingAsync(productBookingCount, currentProductId);

            // Assert

            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async Task RemoveBookingProductToExisingBooking_FailedCount()
        {
            // Arrange
            var entityConverterMock = new Mock<IEntityConverter>();

            var currentId = 1;
            var currentProductId = 1;
            var newProductBookingCount = 10;
            var productBookingCount = 20;

            var bookingDtoElement = new BookingDto { Id = currentId, ProductId = currentProductId, Count = productBookingCount };

            var bookingServiceMock = new Mock<BookingService>(new Mock<IRepository<Booking>>().Object, entityConverterMock.Object);
            bookingServiceMock
                .Setup(bsm => bsm
                    .GetBookingByProductIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(bookingDtoElement);
            bookingServiceMock
                .Setup(bsm => bsm
                    .UpdateBookingAsync(It.Is<int>(i => i == bookingDtoElement.ProductId),
                        It.IsAny<BookingDto>()))
                    .ReturnsAsync(new BookingDto
                    {
                        Id = currentId,
                        ProductId = currentProductId,
                        Count = newProductBookingCount
                    });


            StockService _stockService = new StockService(new Mock<IRepository<Product>>().Object,
               bookingServiceMock.Object, new Mock<IStorageService>().Object, entityConverterMock.Object);

            // Act   
            var result = await _stockService.RemoveProductBookingAsync(productBookingCount, currentProductId);

            // Assert

            Assert.NotEqual(productBookingCount, result.Count);
        }

        [Fact]
        public void RemoveBookingProductToExisingBooking_FailedCountWithException()
        {
            // Arrange
            var entityConverterMock = new Mock<IEntityConverter>();

            var currentId = 1;
            var currentProductId = 1;
            var newProductBookingCount = 50;
            var productBookingCount = 20;

            var bookingDtoElement = new BookingDto { Id = currentId, ProductId = currentProductId, Count = productBookingCount };

            var bookingServiceMock = new Mock<BookingService>(new Mock<IRepository<Booking>>().Object, entityConverterMock.Object);
            bookingServiceMock
                .Setup(bsm => bsm
                    .GetBookingByProductIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(bookingDtoElement);

            StockService _stockService = new StockService(new Mock<IRepository<Product>>().Object,
               bookingServiceMock.Object, new Mock<IStorageService>().Object, entityConverterMock.Object);

            // Act               
            Task removeBookingMethod() => _stockService.RemoveProductBookingAsync(newProductBookingCount, currentProductId);
            var ex = Assert.ThrowsAsync<Exception>(removeBookingMethod);

            //Assert
            Assert.Equal(StockExceptions.ProductNotBookedCountException, ex.Result.Message);
        }

        [Fact]
        public void RemoveBookingProductToExisingBooking_FailedContainWithException()
        {
            // Arrange
            var entityConverterMock = new Mock<IEntityConverter>();

            var currentId = 1;
            var currentProductId = 1;
            var newProductBookingCount = 50;
            var productBookingCount = 20;
            BookingDto? nullBooking = null;

            var bookingDtoElement = new BookingDto { Id = currentId, ProductId = currentProductId, Count = productBookingCount };

            var bookingServiceMock = new Mock<BookingService>(new Mock<IRepository<Booking>>().Object, entityConverterMock.Object);
            bookingServiceMock
                .Setup(bsm => bsm
                    .GetBookingByProductIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(nullBooking);

            StockService _stockService = new StockService(new Mock<IRepository<Product>>().Object,
               bookingServiceMock.Object, new Mock<IStorageService>().Object, entityConverterMock.Object);

            // Act               
            Task removeBookingMethod() => _stockService.RemoveProductBookingAsync(newProductBookingCount, currentProductId);
            var ex = Assert.ThrowsAsync<Exception>(removeBookingMethod);

            //Assert
            Assert.Equal(StockExceptions.ProductNotBookedException, ex.Result.Message);
        }
    }
}
