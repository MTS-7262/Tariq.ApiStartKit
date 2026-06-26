using Application.Abstractions;
using Application.Services;
using Domain.Abstractions;

namespace Application.Features.Authentication.MfaToggle;

public class MfaToggleHandler(ISigninManager signInManager) : IHandler<MfaToggleRequest, Result<MfaToggleResponse>>
{
    public async Task<Result<MfaToggleResponse>> HandleAsync(MfaToggleRequest command, CancellationToken cancellationToken)
    {
        var result = await signInManager.ToggleMfaAsync(command);
        return Result.Success(result);
    }
}
