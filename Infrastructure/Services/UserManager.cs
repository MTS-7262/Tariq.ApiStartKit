using Application.Features.Authentication.Registration;
using Application.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class UserManager(UserManager<ApplicationUser> userManager) : IUserManager
{
    public async Task<bool> UserEmailExistAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        return user != null;
    }

    public async Task RegisterUser(RegisterUserRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        var createResult = await userManager.CreateAsync(user, request.Password);

        if (createResult.Succeeded)
        {
            await userManager.AddToRoleAsync(user, request.Role);
        }
        else
        {
            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
            throw new Exception($"Failed to register user: {errors}");
        }
    }
}