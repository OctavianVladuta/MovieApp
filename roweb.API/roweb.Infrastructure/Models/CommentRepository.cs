using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using roweb.Domain.Models;

namespace roweb.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByMovieIdAsync(int movieId)
        {
            return await _context.Comments
                .Where(c => c.MovieId == movieId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            comment.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Europe/Bucharest"));
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}
