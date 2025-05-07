using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinemaArsivSitesi.Models;
using SinemaArsivSitesi.Services.Movie;
using System.Security.Claims;

namespace SinemaArsivSitesi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("movies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await _movieService.GetAllMovies();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _movieService.AddMovie(movie.Title, movie.Description, movie.PosterUrl, movie.CategoryId);
            if (!result)
            {
                return BadRequest("Film oluşturulamadı");
            }

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            var result = await _movieService.UpdateMovie(id, movie.Title, movie.Description, movie.PosterUrl, movie.CategoryId);
            if (!result)
            {
                return BadRequest("Film güncellenemedi");
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _movieService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }

            var result = await _movieService.DeleteMovie(id);
            if (!result)
            {
                return BadRequest("Film silinemedi");
            }

            return NoContent();
        }
    }
}

