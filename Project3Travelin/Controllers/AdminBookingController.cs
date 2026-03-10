using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.EmailServices;
using Project3Travelin.Services.BookingServices;

namespace Project3Travelin.Controllers
{
    public class AdminBookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly EmailService _emailService;

        public AdminBookingController(IBookingService bookingService, EmailService emailService)
        {
            _bookingService = bookingService;
            _emailService = emailService;
        }

        public async Task<IActionResult> BookingList()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveBooking(string id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            await _bookingService.UpdateBookingStatusAsync(id, "Onaylandı");

            await _emailService.SendBookingConfirmationAsync(
             booking.Email,
             booking.FirstName + " " + booking.LastName,
             booking.TourTitle,
            booking.StartDate,
            booking.TotalPrice
                );

            return RedirectToAction("BookingList");
        }

        [HttpPost]
        public async Task<IActionResult> RejectBooking(string id)
        {
            await _bookingService.UpdateBookingStatusAsync(id, "Reddedildi");
            return RedirectToAction("BookingList");
        }
    }
}
