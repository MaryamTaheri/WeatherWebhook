using WeatherWebhook.Domain.Framework.ValueObjects;

namespace WeatherWebhook.Domain.Entities.Weather;

public partial class Weather
{
    public Weather()
    {
    }

    public Weather(string name, string region, string country, double? lat, double? lon)
    {
        Id = EntityUuid.Generate();
        Name = name;
        Region = region;
        Country = country;
        Lat = lat;
        Lon = lon;
    }
}