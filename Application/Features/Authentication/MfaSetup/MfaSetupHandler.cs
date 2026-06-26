using Application.Abstractions;
using Application.Services;
using Domain.Abstractions;

namespace Application.Features.Authentication.MfaSetup;

public class MfaSetupHandler(ISigninManager signinManager) : IHandler<Result<MfaSetupResponse>>
{
    public async Task<Result<MfaSetupResponse>> HandleAsync(CancellationToken cancellationToken)
    {
        var result = await signinManager.GetAuthenticatorSetupDetailsAsync();
        return Result.Success(result);
    }
}