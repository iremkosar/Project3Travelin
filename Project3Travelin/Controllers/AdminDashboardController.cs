using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.BookingServices;
using Project3Travelin.Services.CommentServices;
using Project3Travelin.Services.TourServices;
using Project3Travelin.ViewModels;

namespace Project3Travelin.Controllers
{
    public class AdminDashboardController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IBookingService _bookingService;
        private readonly ICommentService _commentService;

        public AdminDashboardController(ITourService tourService, IBookingService bookingService, ICommentService commentService)
        {
            _tourService = tourService;
            _bookingService = bookingService;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllTourAsync();
            var bookings = await _bookingService.GetAllBookingsAsync();
            var comments = await _commentService.GetAllCommentAsync();

            var vm = new DashboardViewModel
            {
                TotalTours = tours.Count,
                TotalBookings = bookings.Count,
                TotalComments = comments.Count,
                PendingBookings = bookings.Count(x => x.Status == "Beklemede"),
                RecentTours = tours.OrderByDescending(x => x.StartDate).Take(5).ToList(),
                RecentBookings = bookings.OrderByDescending(x => x.BookingDate).Take(5).ToList(),
                RecentComments = comments.OrderByDescending(x => x.CommentDate).Take(5).ToList()
            };

            return View(vm);
        }
    }
}
