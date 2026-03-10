using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.TourServices;

namespace Project3Travelin.ViewComponents
{
    public class _AboutTopDestinationsComponentPartial : ViewComponent
    {
        private readonly ITourService _tourService;

        public _AboutTopDestinationsComponentPartial(ITourService tourService)
        {
            _tourService = tourService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tours = await _tourService.GetAllTourAsync();
            var destinations = tours.Take(5).ToList();
            return View(destinations);
        }
    }
}
