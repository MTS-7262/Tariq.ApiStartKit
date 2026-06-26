using Application.Abstractions;
using Application.Constants;
using Application.Extensions;
using Domain.Abstractions;

namespace Application.Features.Authentication.MfaDisable;

public class MfaDisableEndpoint : IApiEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("/mfa-disable", async (IHandler<MfaDisableRequest, Result<MfaDisableResponse>> handler,
                MfaDisableRequest command,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.HandleAsync(command, cancellationToken);
                return result.Match(
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: Results.BadRequest);
            })
            .WithTags(ApiTags.Authentication)
            .RequireAuthorization()
            .Produces<MfaDisableResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
    }
}