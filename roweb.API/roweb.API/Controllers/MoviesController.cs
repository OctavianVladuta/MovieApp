using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using roweb.Services.Movies;

namespace roweb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestMovies()
        {
            var movies = await _movieService.GetLatestMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTopMovies()
        {
            var movies = await _movieService.GetTopMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMovies([FromQuery] string? title, [FromQuery] int? genre_ids)
        {
            var movies = await _movieService.SearchMoviesAsync(title, genre_ids);
            return Ok(movies);
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetMovieDetails(int movieId)
        {
            var movieDetails = await _movieService.GetMovieDetailsAsync(movieId);
            return Ok(movieDetails);
        }
    }
}
