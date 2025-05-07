using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SinemaArsivSitesi.Models;

namespace SinemaArsivSitesi.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;


    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
      
        var filmler = new List<Movie>
        {
            new Movie { Id = 1, Title = "Film 1", Category = "Aksiyon", PosterUrl = "/images/film1.jpg" },
            new Movie { Id = 2, Title = "Film 2", Category = "Komedi", PosterUrl = "/images/film2.jpg" },
            new Movie { Id = 3, Title = "Film 3", Category = "Bilim Kurgu", PosterUrl = "/images/film3.jpg" },
            new Movie { Id = 4, Title = "Film 4", Category = "Dram", PosterUrl = "/images/film4.jpg" }
        };

        var kategoriler = new List<string> { "Aksiyon", "Komedi", "Bilim Kurgu", "Dram" };

        var model = new HomeIndexViewModel
        {
            Filmler = filmler,
            Kategoriler = kategoriler
        };

        return View(model);
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
