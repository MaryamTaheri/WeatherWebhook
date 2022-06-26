using WeatherWebhook.Domain.Contracts.API.Weather;
using WeatherWebhook.Domain.Contracts.Commands.Weather;
using WeatherWebhook.Domain.Framework.Contracts.Response;
using WeatherWebhook.Domain.Framework.Services.Handlers;

namespace WeatherWebhook.Domain.Application.Commands.Weather;

public interface IWeatherCommands :
    IApplicationCommandHandler<WeatherInfoCommand, DataResponse<WeatherInfoResultContract>>
{
}