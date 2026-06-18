using Application.Abstractions;
using Application.Services;
using Domain.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Authentication.Registration;


public sealed record RegisterUserRequest(string FirstName, string LastName, string Email, string Password, string Role);
public sealed record RegisterUserResponse(string Email);


public class RegistrationHandler(IUserManager userManager) : IHandler<RegisterUserRequest, Result<RegisterUserResponse>>
{
    public Task<Result<RegisterUserResponse>> HandleAsync(RegisterUserRequest command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}