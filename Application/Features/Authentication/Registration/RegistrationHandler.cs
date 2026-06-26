using Application.Abstractions;
using Application.Services;
using Domain.Abstractions;

namespace Application.Features.Authentication.Registration;


public class RegistrationHandler(IUserManager userManager, IRoleManager roleManager) : IHandler<RegisterUserRequest, Result<RegisterUserResponse>>
{
    public async Task<Result<RegisterUserResponse>> HandleAsync(RegisterUserRequest command, CancellationToken cancellationToken)
    {
        //Check and create role if not exist
        var roleExist = await roleManager.RoleExistAsync(command.Role);
        if (!roleExist)
            await roleManager.CreateRoleAsync(command.Role);


        await userManager.RegisterUser(command);

        return Result.Success(new RegisterUserResponse(command.Email));

    }
}