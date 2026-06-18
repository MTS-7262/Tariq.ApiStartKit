using Application.Abstractions;
using AutoMapper;
using Domain.Abstractions;

namespace Application.Features;

public sealed record GetWeatherForecastRequest;
public sealed record GetWeatherForecastResponse(IEnumerable<WeatherForecastRecord> weather);

public record WeatherForecastRecord(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
public class WeatherForecastHandler(IMapper mapper) : IHandler<GetWeatherForecastRequest, Result<GetWeatherForecastResponse>>
{
    public async Task<Result<GetWeatherForecastResponse>> HandleAsync(GetWeatherForecastRequest command, CancellationToken cancellationToken)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecastRecord
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToList();
        await Task.Delay(100, cancellationToken);

        var response = mapper.Map<GetWeatherForecastResponse>(forecast);

        return Result.Success(response);
    }
}