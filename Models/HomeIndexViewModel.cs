using System.Collections.Generic;

namespace SinemaArsivSitesi.Models
{
    public class HomeIndexViewModel
    {
        public List<Movie> Filmler { get; set; }
        public List<Category> Kategoriler { get; set; }
    }
}
