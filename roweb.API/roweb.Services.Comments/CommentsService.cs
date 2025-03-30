using System.Collections.Generic;
using System.Threading.Tasks;
using roweb.Domain.Models;
using roweb.Infrastructure.Repositories;
using RowebInterview.Services.Comments;

namespace roweb.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentsService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Task<IEnumerable<Comment>> GetCommentsByMovieIdAsync(int movieId)
        {
            return _commentRepository.GetCommentsByMovieIdAsync(movieId);
        }

        public Task AddCommentAsync(Comment comment)
        {
            return _commentRepository.AddCommentAsync(comment);
        }
    }
}
