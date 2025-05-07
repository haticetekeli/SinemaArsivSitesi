using Microsoft.AspNetCore.Identity;

namespace SinemaArsivSitesi.Services.Auth
{
    public static class RoleInitializer
    {
        public static async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var user = await userManager.FindByEmailAsync("admin@email.com");
            if (user != null)
                await userManager.AddToRoleAsync(user, "Admin");
        }
    }
} 