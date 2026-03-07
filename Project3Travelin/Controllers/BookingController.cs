using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.BookingDtos;
using Project3Travelin.Services.BookingServices;
using Project3Travelin.Services.TourServices;
using Project3Travelin.ViewModels;

namespace Project3Travelin.Controllers
{
    public class BookingController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IBookingService _bookingService;

        public BookingController(ITourService tourService, IBookingService bookingService)
        {
            _tourService = tourService;
            _bookingService = bookingService;
        }

        public async Task<IActionResult> CreateBooking(string tourId, int personCount = 1)
        {
            var tour = await _tourService.GetTourByIdAsync(tourId);
            var viewModel = new BookingViewModel
            {
                Tour = tour,
                PersonCount = personCount
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto dto)
        {
            
                var tour = await _tourService.GetTourByIdAsync(dto.TourId);
            dto.StartDate = tour.StartDate;
            dto.EndDate = tour.EndDate;
            await _bookingService.CreateBookingAsync(dto);
            return RedirectToAction("TourDetail", "Tour", new { id = dto.TourId });

        }

        public IActionResult BookingConfirmation(string tourTitle)
        {
            ViewBag.TourTitle = tourTitle;
            return View();
        }
    }
}
