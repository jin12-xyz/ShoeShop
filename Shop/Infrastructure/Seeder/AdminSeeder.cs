using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shop.Models.Domain;

namespace Shop.Infrastructure.Data.Seeders
{
    public static class AdminSeeder
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config)
        {
            // ================================
            // Seed Roles
            // ================================
            string[] roles = { "Admin", "Customer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // ================================
            // Get Admin Credentials
            // ================================
            var adminEmail = config["AdminSettings:Email"];
            var adminPassword = config["AdminSettings:Password"];

            if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
                throw new Exception("Admin credentials are not configured properly.");

            // ================================
            // Seed Admin User
            // ================================
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var admin = new ApplicationUser
                {
                    Fullname = "Admin",
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(admin, adminPassword);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
                else
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}