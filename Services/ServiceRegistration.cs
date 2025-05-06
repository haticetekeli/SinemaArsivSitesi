using SinemaArsivSitesi.Services.Auth;
using SinemaArsivSitesi.Services.Category;
using SinemaArsivSitesi.Services.Movie;

namespace SinemaArsivSitesi.Services
{
    public static class ServiceRegistration
    {
        public static void BusinessRegistration(this IServiceCollection service)
        {
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<IMovieService, MovieService>();
        }
    }
}
