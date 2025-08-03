using Microsoft.AspNetCore.Mvc;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpPost("movies")]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
        {
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                YearOfRelease = request.YearOfRelease,
                Genre = request.Genre.ToList()
            };
            
            await _movieRepository.CreateAsync(movie);

            var movieResponse = new
            {
                movie.Id,
                movie.Title,
                movie.YearOfRelease,
                Genre = movie.Genre
            };

            return Created($"/api/movies/{movie.Id}", movie);
        }
    }
}
