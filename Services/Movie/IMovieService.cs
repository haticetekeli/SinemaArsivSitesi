using SinemaArsivSitesi.Models;

namespace SinemaArsivSitesi.Services.Movie
{
    public interface IMovieService
    {
        Task<bool> AddMovie(string title, string description, string posterUrl, int categoryId);
        Task<bool> UpdateMovie(int id, string title, string description, string posterUrl, int categoryId);
        Task<bool> DeleteMovie(int id);
        Task<List<Models.Movie>> GetAllMovies();
        Task<Models.Movie> GetMovieById(int id);
        Task<bool> IsMovieExists(string title);
    }
}
