using WeatherWebhook.Domain.Contracts.API.Weather;
using WeatherWebhook.Domain.Framework.Contracts.Response;
using WeatherWebhook.Domain.Framework.Services;

namespace WeatherWebhook.Domain.Contracts.Commands.Weather;

public class WeatherInfoCommand : IApplicationCommand<DataResponse<WeatherInfoResultContract>>
{
}