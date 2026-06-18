using Application.Abstractions;
using Application.Abstractions.Data.DataSeeder;

namespace Application.SystemTasks.SeedEndpoint;

internal sealed class SeedAdminEndpoint : IApiEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("/seed-admin", async (IIdentityDataSeeder seeder, CancellationToken cancellationToken) =>
            {
                try
                {
                    await seeder.SeedAdminUserAsync(cancellationToken);
                    return Results.Ok(new { message = "Administrative role and user seeded successfully." });
                }
                catch (Exception ex)
                {
                    return Results.Problem(
                        detail: ex.Message,
                        statusCode: StatusCodes.Status500InternalServerError,
                        title: "Seeding Failed");
                }
            })
            .ExcludeFromDescription();
    }
}