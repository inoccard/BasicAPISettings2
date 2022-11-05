using BasicAPISettings.Api.Domain.SeedWorks;

namespace BasicAPISettings.Api.Domain.Models.WeatherForecastAggregate.Entities;

public class WeatherForecast : EntityInt32Id
{
    public DateTime Date { get; private set; }

    public int TemperatureC { get; private set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; private set; }

    public WeatherForecast(DateTime date, int temperatureC, string? summary)
    {
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
    }

    public void Update(DateTime date, int temperatureC, string? summary)
    {
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
    }
}
