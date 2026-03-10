using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.TourServices;

namespace Project3Travelin.ViewComponents
{
    public class _AboutPopularPlacesComponentPartial : ViewComponent
    {
        private readonly ITourService _tourService;

        public _AboutPopularPlacesComponentPartial(ITourService tourService)
        {
            _tourService = tourService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tours = await _tourService.GetAllTourAsync();  
            var featured = tours.Take(5).ToList();
            return View(featured);
        }
    }
}
