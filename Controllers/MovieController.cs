using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinemaArsivSitesi.Services.Movie;
using SinemaArsivSitesi.Services.Category;

namespace SinemaArsivSitesi.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IUserFavoriteMovieService _favoriteService;
        private readonly ICategoryService _categoryService;

        public MovieController(
            IMovieService movieService,
            IUserFavoriteMovieService favoriteService,
            ICategoryService categoryService)
        {
            _movieService = movieService;
            _favoriteService = favoriteService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllMovies();
            return View(movies);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllCategories();
            return View();
        }

        [HttpPost]
        [Route("AddMovies")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(string title, string description, string posterUrl, int categoryId)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Film adı boş olamaz.");
            }

            var result = await _movieService.AddMovie(title, description, posterUrl, categoryId);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest("Bu film zaten eklenmiş veya bir hata oluştu.");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _categoryService.GetAllCategories();
            return View(movie);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, string title, string description, string posterUrl, int categoryId)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Film adı boş olamaz.");
            }

            var result = await _movieService.UpdateMovie(id, title, description, posterUrl, categoryId);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest("Bu film adı zaten kullanılıyor veya bir hata oluştu.");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _movieService.DeleteMovie(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest("Film silinirken bir hata oluştu.");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int movieId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = await _favoriteService.AddToFavorites(userId, movieId);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest("Film zaten favorilerinizde veya bir hata oluştu.");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int movieId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = await _favoriteService.RemoveFromFavorites(userId, movieId);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest("Film favorilerinizden kaldırılırken bir hata oluştu.");
        }

        [Authorize]
        public async Task<IActionResult> MyFavorites()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var favorites = await _favoriteService.GetUserFavoriteMovies(userId);
            return View(favorites);
        }
    }
}

