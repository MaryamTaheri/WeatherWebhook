using WeatherWebhook.Domain.ExternalServices.WeatherWebService.Dtos;

namespace WeatherWebhook.Domain.ExternalServices.WeatherWebService;

public interface IWeatherInfoServiceClient
{
    Task<WeatherInfoResponseDto> GetLastInformationAsync(CancellationToken cancellationToken);
}