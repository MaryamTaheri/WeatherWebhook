
namespace WeatherWebhook.Domain.ExternalServices.WeatherWebService.Dtos;

public class WeatherInfoResponseDto
{
    public string Name { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public double? Lat { get; set; }
    public double? Lon { get; set; }
}