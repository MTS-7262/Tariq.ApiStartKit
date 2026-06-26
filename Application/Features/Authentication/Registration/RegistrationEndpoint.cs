using Application.Abstractions;
using Application.Constants;
using Application.Extensions;
using Domain.Abstractions;

namespace Application.Features.Authentication.Registration;

internal sealed class RegistrationEndpoint : IApiEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("/register", async (IHandler<RegisterUserRequest, Result<RegisterUserResponse>> handler, 
                                               RegisterUserRequest command, 
                                               CancellationToken cancellationToken) =>
            {
                var result = await handler.HandleAsync(command, cancellationToken);
                return result.Match(
                    onSuccess: () => Results.Ok(result.Value), 
                    onFailure: Results.BadRequest);
            })
            .WithTags(ApiTags.Authentication)
            .Produces<RegisterUserResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
    }
}