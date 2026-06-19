using Application.Abstractions;
using Application.Services;
using Domain.Abstractions;

namespace Application.Features.Authentication.Registration;


public sealed record RegisterUserRequest(string FirstName, string LastName, string Email, string Password, string Role);
public sealed record RegisterUserResponse(string Email);


public class RegistrationHandler(IUserManager userManager, IRoleManager roleManager) : IHandler<RegisterUserRequest, Result<RegisterUserResponse>>
{
    public async Task<Result<RegisterUserResponse>> HandleAsync(RegisterUserRequest command, CancellationToken cancellationToken)
    {
        //Check and create role if not exist
        var roleExist = await roleManager.RoleExistAsync(command.Role);
        if (!roleExist)
            await roleManager.CreateRoleAsync(command.Role);

        //Check and register new user
        var userExist = await userManager.UserEmailExistAsync(command.Email);
        if (userExist)
            return Result.Failure<RegisterUserResponse>(AuthenticationErrors.UserExit(command.Email));

        await userManager.RegisterUser(command);

        return Result.Success(new RegisterUserResponse(command.Email));

    }
}