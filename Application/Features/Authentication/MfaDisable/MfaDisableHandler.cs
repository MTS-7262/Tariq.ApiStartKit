using Application.Abstractions;
using Application.Services;
using Domain.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Authentication.MfaDisable;

public class MfaDisableHandler(ISigninManager signInManager) : IHandler<MfaDisableRequest, Result<MfaDisableResponse>>
{
    public async Task<Result<MfaDisableResponse>> HandleAsync(MfaDisableRequest command, CancellationToken cancellationToken)
    {
        var result = await signInManager.DisableMfaAsync(command);
        return Result.Success(result);
    }
}