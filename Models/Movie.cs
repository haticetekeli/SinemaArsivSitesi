using SinemaArsivSitesi.Data;
using SinemaArsivSitesi.Models; 
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SinemaArsivSitesi.Models
{
    public class Movie  :BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; }  
        public string PosterUrl { get; set; }  
        public int CategoryId { get; set; }
        public Category Category { get; set; } 
    }
}
