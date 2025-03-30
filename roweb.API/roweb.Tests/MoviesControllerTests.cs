using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using roweb.API.Controllers;
using roweb.Domain.Dto;
using roweb.Services.Movies;
using Xunit;

namespace roweb.Tests.Controllers
{
    public class MoviesControllerTests
    {
        [Fact]
        public async Task GetLatestMovies_ReturnsOkResult_WithMovies()
        {
            var mockMovieService = new Mock<IMovieService>();
            var movies = new List<MovieDetailsDto>
                {
                    new MovieDetailsDto { Id = 1, Title = "Test Movie", GenreIds = new List<int>{ 28 } }
                };
            mockMovieService.Setup(s => s.GetLatestMoviesAsync()).ReturnsAsync(movies);
            var controller = new MoviesController(mockMovieService.Object);

            var result = await controller.GetLatestMovies();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnMovies = Assert.IsAssignableFrom<IEnumerable<MovieDetailsDto>>(okResult.Value);
            Assert.Single(returnMovies);
        }
    }
}
