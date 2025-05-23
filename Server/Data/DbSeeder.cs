using Microsoft.AspNetCore.Identity;

namespace BlazorBookHub.Server.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider provider)
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdminUserAsync(IServiceProvider provider)
        {
            var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "admin@example.com";
            string adminPassword = "Admin123!";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync("Admin"))
                        await roleManager.CreateAsync(new IdentityRole("Admin"));

                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

    }
}
