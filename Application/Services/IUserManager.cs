using Application.Features.Authentication.Registration;
using Application.Features.User.GetAllUsers;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public interface IUserManager
{
    IQueryable<Users> Query();
    Task<bool> UserEmailExistAsync(string email);
    Task RegisterUser(RegisterUserRequest request);

}