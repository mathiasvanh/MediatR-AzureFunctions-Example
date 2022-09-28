namespace MediatR.Example.OpenWeatherMap.Services.Base;

public class OpenWeatherMapOptions
{
    public string? ApiKey { get; set; }

    public Uri BaseAddress { get; set; } = new Uri("https://api.openweathermap.org/data/");

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            throw new InvalidOperationException("No api key specified.");
        }

        if (BaseAddress == null)
        {
            throw new InvalidOperationException("No base address specified.");
        }
    }
}