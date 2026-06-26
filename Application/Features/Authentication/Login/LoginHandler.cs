using Application.Abstractions;
using Application.Services;
using Domain.Abstractions;
using Domain.Abstractions.Errors;

namespace Application.Features.Authentication.Login;

public class LoginHandler(ISigninManager signinManager) : IHandler<LoginRequest, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> HandleAsync(LoginRequest command, CancellationToken cancellationToken)
    {
        var result = await signinManager.PasswordSignInAsync(command);

        if (result.Succeeded)
            return Result.Success(new LoginResponse(string.Empty, string.Empty));

        if (result.IsLockedOut)
            return Result.Failure<LoginResponse>(new Error(StatusCodes.Status423Locked.ToString(), "User Locked Contact to Administrator"));

        var validProviders = await signinManager.RequiredTwoFactorAsync();

        return Result.Success(new LoginResponse(validProviders.Message, string.Empty, validProviders.Provider));
    }
}