using Marketplace.Models.Dto.Stock;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Stocks
{
    public interface IBookingService
    {
        Task<BookingDto> CreateBookingAsync(BookingDto bookingDto);

        Task<BookingDto> UpdateBookingAsync(int id, BookingDto bookingDto);
        Task<BookingDto> GetBookingByProductIdAsync(int id);
    }
}
