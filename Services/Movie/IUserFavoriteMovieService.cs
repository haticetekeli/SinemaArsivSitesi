using SinemaArsivSitesi.Models;

namespace SinemaArsivSitesi.Services.Movie
{
    public interface IUserFavoriteMovieService
    {
        Task<bool> AddToFavorites(string userId, int movieId);
        Task<bool> RemoveFromFavorites(string userId, int movieId);
        Task<List<Models.Movie>> GetUserFavoriteMovies(string userId);
        Task<bool> IsMovieInFavorites(string userId, int movieId);
    }
} 