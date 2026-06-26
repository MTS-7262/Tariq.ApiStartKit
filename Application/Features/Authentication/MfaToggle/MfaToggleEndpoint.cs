using Application.Abstractions;
using Application.Constants;
using Application.Extensions;
using Domain.Abstractions;

namespace Application.Features.Authentication.MfaToggle;

public class MfaToggleEndpoint : IApiEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapGet("/mfa-toggle", async (IHandler<MfaToggleRequest, Result<MfaToggleResponse>> handler,
                MfaToggleRequest command,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.HandleAsync(command, cancellationToken);
                return result.Match(
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: Results.BadRequest);
            })
            .WithTags(ApiTags.Authentication)
            .Produces<MfaToggleResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
    }
}