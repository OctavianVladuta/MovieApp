using System.Collections.Generic;
using System.Threading.Tasks;
using roweb.Domain.Dto;

namespace roweb.Services.Movies
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDetailsDto>> GetLatestMoviesAsync();
        Task<IEnumerable<MovieDetailsDto>> GetTopMoviesAsync();
        Task<IEnumerable<MovieDetailsDto>> SearchMoviesAsync(string name, int? genreId);
        Task<MovieDetailsDto> GetMovieDetailsAsync(int movieId);
    }
}
