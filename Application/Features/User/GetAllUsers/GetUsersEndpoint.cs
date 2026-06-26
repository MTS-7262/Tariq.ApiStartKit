using Application.Abstractions;
using Application.Constants;
using Application.Extensions;
using Domain.Abstractions;

namespace Application.Features.User.GetAllUsers;

public class GetUsersEndpoint : IApiEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapGet("/users", async ([AsParameters] GetPaginatedUsersQuery query,
                IHandler<GetPaginatedUsersQuery, Result<PaginatedList<UserResponseDto>>> handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.HandleAsync(query, cancellationToken);
                return result.Match(
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: Results.BadRequest);
            })
            .WithTags(ApiTags.User)
            .RequireAuthorization()
            .Produces<PaginatedList<UserResponseDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
    }
}