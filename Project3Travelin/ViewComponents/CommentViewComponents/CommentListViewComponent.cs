using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.CommentDtos;

namespace Project3Travelin.ViewComponents.CommentViewComponents
{
    public class CommentListViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(List<ResultCommentListByTourIdDto> comments)
        {
            return View(comments);
        }
    }
}
