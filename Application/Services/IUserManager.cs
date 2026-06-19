using Application.Features.Authentication.Registration;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public interface IUserManager
{
    Task<bool> UserEmailExistAsync(string email);
    Task RegisterUser(RegisterUserRequest request);
}