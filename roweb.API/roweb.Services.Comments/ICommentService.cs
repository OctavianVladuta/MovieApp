using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using roweb.Domain.Models;

namespace RowebInterview.Services.Comments
{
    public interface ICommentsService
    {
        Task<IEnumerable<Comment>> GetCommentsByMovieIdAsync(int movieId);
        Task AddCommentAsync(Comment comment);
    }
}
