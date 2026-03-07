using Project3Travelin.Dtos.BookingDtos;
using Project3Travelin.Entities;

namespace Project3Travelin.Services.BookingServices
{
    public interface IBookingService
    {
        Task CreateBookingAsync(CreateBookingDto dto);
        Task<List<ResultBookingDto>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(string id);
        Task UpdateBookingStatusAsync(string id, string status);
        Task<List<ResultBookingDto>> GetBookingsByTourIdAsync(string tourId);
    }
}

