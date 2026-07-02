using Application.Features.Authentication.Registration;
using Domain.Entities;

namespace Application.Services;

public interface IUserManager
{
    Task<Users> GetUserByIdAsync(Guid id);
    IQueryable<Users> Query();
    Task<bool> UserEmailExistAsync(string email);
    Task RegisterUser(RegisterUserRequest request);

}