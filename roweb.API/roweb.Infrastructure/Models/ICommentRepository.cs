using System.Collections.Generic;
using System.Threading.Tasks;
using roweb.Domain.Models;

namespace roweb.Infrastructure.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByMovieIdAsync(int movieId);
        Task AddCommentAsync(Comment comment);
    }
}
