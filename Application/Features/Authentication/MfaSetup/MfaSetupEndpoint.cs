using Application.Abstractions;
using Application.Constants;
using Application.Extensions;
using Domain.Abstractions;

namespace Application.Features.Authentication.MfaSetup;

public class MfaSetupEndpoint : IApiEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapGet("/mfa-setup", async (IHandler<Result<MfaSetupResponse>> handler,
                                                         CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(cancellationToken);
            return result.Match(
                onSuccess: () => Results.Ok(result.Value),
                onFailure: Results.BadRequest);
        })
        .WithTags(ApiTags.Authentication)
        .RequireAuthorization()
        .Produces<MfaSetupResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}