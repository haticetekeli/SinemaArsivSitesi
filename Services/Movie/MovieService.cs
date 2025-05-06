using Microsoft.EntityFrameworkCore;
using SinemaArsivSitesi.Data;
using SinemaArsivSitesi.Models;

namespace SinemaArsivSitesi.Services.Movie
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddMovie(string title, string description, string posterUrl, int categoryId)
        {
            try
            {
                if (await IsMovieExists(title))
                {
                    return false;
                }

                var movie = new Models.Movie
                {
                    Title = title,
                    Description = description,
                    PosterUrl = posterUrl,
                    CategoryId = categoryId,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedById = 1 
                };

                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateMovie(int id, string title, string description, string posterUrl, int categoryId)
        {
            try
            {
                var movie = await _context.Movies.FindAsync(id);
                if (movie == null) return false;

              
                if (movie.Title != title && await IsMovieExists(title))
                {
                    return false;
                }

                movie.Title = title;
                movie.Description = description;
                movie.PosterUrl = posterUrl;
                movie.CategoryId = categoryId;
                movie.UpdatedDate = DateTime.Now;
                movie.UpdatedById = 1; 

                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteMovie(int id)
        {
            try
            {
                var movie = await _context.Movies.FindAsync(id);
                if (movie == null) return false;

                movie.IsDeleted = true;
                movie.UpdatedDate = DateTime.Now;
                movie.UpdatedById = 1;

                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Models.Movie>> GetAllMovies()
        {
            return await _context.Movies
                .Include(m => m.Category)
                .Where(m => !m.IsDeleted)
                .ToListAsync();
        }

        public async Task<Models.Movie> GetMovieById(int id)
        {
            return await _context.Movies
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
        }

        public async Task<bool> IsMovieExists(string title)
        {
            return await _context.Movies
                .AnyAsync(m => m.Title.ToLower() == title.ToLower() && !m.IsDeleted);
        }
    }
}
