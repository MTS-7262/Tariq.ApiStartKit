using Application.Abstractions;
using Application.Services;
using Domain.Abstractions;

namespace Application.Features.Authentication.Mfa;

public class MfaHandler(ISigninManager signInManager) : IHandler<MfaVerificationRequest, Result<MfaVerificationResponse>>
{
    public async Task<Result<MfaVerificationResponse>> HandleAsync(MfaVerificationRequest command, CancellationToken cancellationToken)
    {
        var result = await signInManager.TwoFactorSignInAsync(command);

        return Result.Success(new MfaVerificationResponse(result.Token, result.RefreshToken));
    }
}