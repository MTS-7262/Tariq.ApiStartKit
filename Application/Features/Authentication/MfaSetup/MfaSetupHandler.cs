using Application.Abstractions;
using Application.Services;
using Domain.Abstractions;

namespace Application.Features.Authentication.MfaSetup;

public class MfaSetupHandler(ISigninManager signInManager) : IHandler<Result<MfaSetupResponse>>
{
    public async Task<Result<MfaSetupResponse>> HandleAsync(CancellationToken cancellationToken)
    {
        var result = await signInManager.GetAuthenticatorSetupDetailsAsync();
        return Result.Success(result);
    }
}