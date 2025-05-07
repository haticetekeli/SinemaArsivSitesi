using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SinemaArsivSitesi.Models;
using SinemaArsivSitesi.Services.Category;
using SinemaArsivSitesi.Services.Movie;

namespace SinemaArsivSitesi.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMovieService _movieService;
    private readonly ICategoryService _categoryService;

    public HomeController(ILogger<HomeController> logger, IMovieService movieService, ICategoryService categoryService)
    {
        _logger = logger;
        _movieService = movieService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var filmler = new List<Movie>
        {
            new Movie { Title = "Film1", Description = "Bilim kurgu filmi", PosterUrl = "/images/Film4.jpg", Category = new Category { Name = "Aksiyon" } },
            new Movie { Title = "Film2", Description = "Aksiyon filmi", PosterUrl = "/images/Film2.jpg", Category = new Category { Name = "Aksiyon" } },
            new Movie { Title = "Film3", Description = "Suç filmi", PosterUrl = "/images/Film3.jpg", Category = new Category { Name = "Suç" } },
            new Movie { Title = "Film4", Description = "Drama filmi", PosterUrl = "/images/Film4.jpg", Category = new Category { Name = "Drama" } }
        };

        var viewModel = new HomeIndexViewModel
        {
            Filmler = filmler,
            Kategoriler = await _categoryService.GetAllCategories()
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
