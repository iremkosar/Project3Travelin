using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.CommentServices;

namespace Project3Travelin.Controllers
{
    public class AdminCommentController : Controller
    {
        private readonly ICommentService _commentService;

        public AdminCommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> CommentList()
        {
            var comments = await _commentService.GetAllCommentAsync();
            return View(comments);
        }

        public async Task<IActionResult> DeleteComment(string id)
        {
            await _commentService.DeleteCommentAsync(id);
            return RedirectToAction("CommentList");
        }

        public async Task<IActionResult> ToggleStatus(string id)
        {
            await _commentService.ToggleStatusAsync(id);
            return RedirectToAction("CommentList");
        }
    }
}
