namespace WeatherWebhook.Domain.Contracts.API.Weather;

public class WeatherInfoResultContract
{
    public string Name { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public double? Lat { get; set; }
    public double? Lon { get; set; }
}