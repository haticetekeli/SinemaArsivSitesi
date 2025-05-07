namespace SinemaArsivSitesi.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Movie> Movies { get; set; } 
    }
}



