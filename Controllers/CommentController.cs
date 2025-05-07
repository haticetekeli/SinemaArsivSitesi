using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinemaArsivSitesi.Models;
using SinemaArsivSitesi.Services.Comments;

namespace SinemaArsivSitesi.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int filmId, string icerik)
        {
            var yorum = new Comment
            {
                Id = filmId,
                Text = icerik,
            };
            await _commentService.AddCommentAsync(yorum);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _commentService.DeleteCommentAsync(id);
            return RedirectToAction("Index", "Home");
        }

        
    }
}