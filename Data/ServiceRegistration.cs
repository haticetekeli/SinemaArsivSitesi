using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SinemaArsivSitesi.Data
{
    public static class ServiceRegistration
    {
        public static void DataAccessRegistration(this IServiceCollection services)
        {
            var connectionString = ("User ID=postgres;Password=12345;Server=localhost;Port=5432;Database=CinemaArchive;Pooling=true;");

            services.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseNpgsql(connectionString, opt =>
                {
                    opt.CommandTimeout(120);
                });
                x.EnableSensitiveDataLogging();
            });
            services.TryAddScoped<DbContext, ApplicationDbContext>();
        }
    }
}
