using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using roweb.Domain.Dto;
using roweb.Domain.Models;
using RowebInterview.Services.Comments;

namespace roweb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;
        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetComments(int movieId)
        {
            var comments = await _commentsService.GetCommentsByMovieIdAsync(movieId);
            return Ok(comments);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] CreateCommentDto commentValidation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (User?.Identity?.IsAuthenticated == false)
                return Unauthorized("Trebuie să fii autentificat pentru a posta un comentariu.");

            Comment comment = new Comment
            {
                MovieId = commentValidation.MovieId,
                Text = commentValidation.Text,
                UserName = User?.Identity?.Name
            };


            await _commentsService.AddCommentAsync(comment);
            return Ok(new { message = comment.UserName });
        }
    }
}
