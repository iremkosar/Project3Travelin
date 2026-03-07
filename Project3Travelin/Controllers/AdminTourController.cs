using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Services.BookingServices;
using Project3Travelin.Services.TourServices;

namespace Project3Travelin.Controllers
{
    public class AdminTourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IBookingService _bookingService;

     
        public AdminTourController(ITourService tourService, IBookingService bookingService)
        {
            _tourService = tourService;
            _bookingService = bookingService;
        }

        public async Task<IActionResult> TourList()
        {
            var values=await _tourService.GetAllTourAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateTour()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTour(CreateTourDto createTourDto)
        {
            await _tourService.CreateTourAsync(createTourDto);
            return RedirectToAction("TourList");
        }
        public async Task<IActionResult> DeleteTour(string id)
        {
            await _tourService.DeleteTourAsync(id);
            return RedirectToAction("TourList");
        }
        public async Task<IActionResult> UpdateTour(string id)
        {
            var value=await _tourService.GetTourByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTour(UpdateTourDto updateTourDto)
        {
            await _tourService.UpdateTourAsync(updateTourDto);
            return RedirectToAction("TourList");
        }
        public async Task<IActionResult> TourCustomers(string id)
        {
            var tour = await _tourService.GetTourByIdAsync(id);
            var bookings = await _bookingService.GetBookingsByTourIdAsync(id);
            ViewBag.TourTitle = tour.Title;
            ViewBag.TourId = id;
            return View(bookings);
        }
    }
}
