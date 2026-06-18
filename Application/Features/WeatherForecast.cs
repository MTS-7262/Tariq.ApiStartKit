using Application.Abstractions;
using Application.Extensions;
using Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features;

internal sealed class WeatherForecast:IApiEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapGet("/weatherforecast", async (IHandler<GetWeatherForecastRequest, Result<GetWeatherForecastResponse>> handler, CancellationToken cancellationToken) =>
            {
                var result = await handler.HandleAsync(new GetWeatherForecastRequest(), cancellationToken);
                return result.Match(
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: Results.BadRequest);
            })
            .WithName("GetWeatherForecast")
            .Produces<GetWeatherForecastResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
    }
}

