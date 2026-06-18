
using Application.Abstractions.Data.DataSeeder;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.DataSeeder;

public class IdentityDataSeeder(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager) 
    : IIdentityDataSeeder
{
    public async Task SeedAdminUserAsync(CancellationToken cancellationToken = default)
    {
        const string adminRoleName = "Admin";
        const string adminEmail = "admin@yourdomain.com";
        const string password = "SecureAdmin123!";

        // 1. Ensure the "Admin" role exists
        if (!await roleManager.RoleExistsAsync(adminRoleName))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRoleName));
        }

        // 2. Ensure the Admin user exists
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                FirstName = "System",
                LastName = "Administrator",
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            var createResult = await userManager.CreateAsync(adminUser, password);

            if (createResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, adminRoleName);
            }
            else
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                throw new Exception($"Failed to seed Admin user: {errors}");
            }
        }
    }
}