using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Services.CommentServices;
using Project3Travelin.Services.TourServices;
using Project3Travelin.ViewModels;

namespace Project3Travelin.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly ICommentService _commentService;

        public TourController(ITourService tourService, ICommentService commentService)
        {
            _tourService = tourService;
            _commentService = commentService;
        }

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
        
        public async Task<IActionResult> TourList()
        {
            var values = await _tourService.GetAllTourAsync();
            return View(values);
        }
        public async Task<IActionResult> TourDetail(string id)
        {
            var tour = await _tourService.GetTourByIdAsync(id);
            var comments = await _commentService.GetCommentsByTourId(id);

            var viewModel = new TourDetailViewModel
            {
                Tour = tour,
                Comments = comments
            };

            return View(viewModel);
        }
    }
}
