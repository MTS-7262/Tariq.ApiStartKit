using Application.Services;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class RoleManager(RoleManager<IdentityRole> roleManager) : IRoleManager
{
    public async Task<bool> RoleExistAsync(string roleName)
    {
        var roleExit = await roleManager.RoleExistsAsync(roleName);
        return roleExit;
    }

    public async Task CreateRoleAsync(string roleName) =>
        await roleManager.CreateAsync(new IdentityRole(roleName));

}