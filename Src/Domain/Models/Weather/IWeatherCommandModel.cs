using WeatherWebhook.Domain.Contracts.API.Weather;
using WeatherWebhook.Domain.Contracts.Commands.Weather;
using WeatherWebhook.Domain.Framework.Contracts.Response;
using WeatherWebhook.Domain.Framework.Models;

namespace WeatherWebhook.Domain.Models.Weather;

public interface IWeatherCommandModel : ICommandModel
{
    Task<DataResponse<WeatherInfoResultContract>> CreateAndGetWeatherInfoAsync(WeatherInfoCommand command, CancellationToken cancellationToken);
}