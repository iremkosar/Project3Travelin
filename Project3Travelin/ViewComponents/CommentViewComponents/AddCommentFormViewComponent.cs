using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.CommentDtos;

namespace Project3Travelin.ViewComponents.CommentViewComponents
{
    public class AddCommentFormViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(string tourId)
        {
            return View(new CreateCommentDto { TourId = tourId });
        }
    }
}
