using Application.Abstractions;
using Application.Constants;
using Domain.Abstractions;

namespace Application.Features.Authentication.Registration;

internal sealed class RegistrationEndpoint : IApiEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("/register", async (IHandler<RegisterUserRequest, Result<RegisterUserResponse>> handler, RegisterUserRequest command, CancellationToken cancellationToken) =>
            {

            })
            .WithTags(ApiTags.Authentication)
            .Produces<RegisterUserResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
    }
}