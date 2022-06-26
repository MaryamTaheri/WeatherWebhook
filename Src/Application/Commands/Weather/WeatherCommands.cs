using WeatherWebhook.Domain.Application.Commands.Weather;
using WeatherWebhook.Domain.Contracts.API.Weather;
using WeatherWebhook.Domain.Contracts.Commands.Weather;
using WeatherWebhook.Domain.Framework.Contracts.Response;
using WeatherWebhook.Domain.Models.Weather;

namespace WeatherWebhook.Application.Commands.Weather;

public class WeatherCommands : CommandsBase, IWeatherCommands
{
    private readonly IWeatherCommandModel _weatherCommandModel;

    public WeatherCommands(IWeatherCommandModel weatherCommandModel)
    {
        _weatherCommandModel = weatherCommandModel;
    }

    public async Task<DataResponse<WeatherInfoResultContract>> Handle(WeatherInfoCommand command, CancellationToken cancellationToken)
        => await _weatherCommandModel.CreateAndGetWeatherInfoAsync(command, cancellationToken);
}