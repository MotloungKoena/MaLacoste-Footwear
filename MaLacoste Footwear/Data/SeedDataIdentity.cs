using MaLacoste_Footwear.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MaLacoste_Footwear.Data
{
    public static class SeedDataIdentity
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Lacoste 2185";
        private const string adminEmail = "admin@lacoste.co.za";
        private const string adminRole = "Admin";

       public static async void EnsurePopulated(IApplicationBuilder app)
        {
            SneakerIdentityDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<SneakerIdentityDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            UserManager<AppUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<AppUser>>();

            RoleManager<IdentityRole> roleManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            if (await userManager.FindByNameAsync(adminUser) == null)
            {
                if (await roleManager.FindByNameAsync(adminRole) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(adminRole));
                }

                AppUser user = new AppUser
                {
                    UserName = adminUser,
                    Email = adminEmail,
                };

                IdentityResult result = await userManager.CreateAsync(user, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, adminRole);
                }
            }
        }

    }
}
