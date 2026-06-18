using AutoMapper;

namespace Application.Features;

public class WeatherProfile:Profile
{
    public WeatherProfile()
    {
        CreateMap<IEnumerable<WeatherForecastRecord>, GetWeatherForecastResponse>()
            .ForCtorParam(nameof(GetWeatherForecastResponse.weather),
                opt => opt.MapFrom(src => src));
    }
}