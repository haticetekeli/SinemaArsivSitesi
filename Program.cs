
using Microsoft.AspNetCore.Identity;
using SinemaArsivSitesi.Data;
using SinemaArsivSitesi.Services;
var builder = WebApplication.CreateBuilder(args);



builder.Services.BusinessRegistration();
builder.Services.DataAccessRegistration();
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleInitializer.CreateRolesAsync(services); 
}

app.Run();

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


