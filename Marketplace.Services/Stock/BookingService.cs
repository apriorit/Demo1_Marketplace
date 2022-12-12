using Marketplace.Contracts;
using Marketplace.Contracts.Stocks;
using Marketplace.Data.Infrastructure;
using Marketplace.Data.Models.Stocks;
using Marketplace.Models.Dto.Stock;
using System.Threading.Tasks;

namespace Marketplace.Services.Stock
{
    public class BookingService : DomainService<int, Booking, BookingDto>, IBookingService
    {
        public BookingService(IRepository<Booking> repository, IEntityConverter entityConverter)
            : base(repository, entityConverter)
        {
        }

        public virtual Task<BookingDto> CreateBookingAsync(BookingDto bookingDto)
        {
            return CreateAsync(bookingDto);
        }

        public virtual Task<BookingDto> UpdateBookingAsync(int id, BookingDto bookingDto)
        {
            return UpdateAsync(id, bookingDto);
        }

        public virtual async Task<BookingDto> GetBookingByProductIdAsync(int productId)
        {
            var entity = await _repository.GetFirstOrDefaultAsync(model => model.ProductId == productId);
            return EntityConverter.ConvertTo<Booking, BookingDto>(entity);
        }
    }
}
