using Application.Abstractions;
using Application.Constants;
using Application.Extensions;
using Domain.Abstractions;

namespace Application.Features.Authentication.Login;

internal sealed class LoginEndpoint : IApiEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("/login", async (IHandler<LoginRequest, Result<LoginResponse>> handler, LoginRequest command, CancellationToken cancellationToken) =>
            {
                var result = await handler.HandleAsync(command, cancellationToken);
                return result.Match(
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: Results.BadRequest);
            })
            .WithTags(ApiTags.Authentication)
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
    }
}