using Application.Abstractions;
using Application.Constants;
using Application.Extensions;
using Domain.Abstractions;

namespace Application.Features.User.GetUserById;

internal class GetUserByIdEndPoint : IApiEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapGet("/user/{userId:guid}", async (Guid userId, IHandler<Guid, Result<UserResponseDto>> handler, CancellationToken cancellationToken) =>
            {
                var result = await handler.HandleAsync(userId, cancellationToken);
                return result.Match(
                    onSuccess: () => Results.Ok(result),
                    onFailure: Results.BadRequest
                );
            })
            .WithTags(ApiTags.User)
            .Produces<UserResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
    }
}
