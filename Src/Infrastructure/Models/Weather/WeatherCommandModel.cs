using WeatherWebhook.Domain.Contracts.API.Weather;
using WeatherWebhook.Domain.Contracts.Commands.Weather;
using WeatherWebhook.Domain.ExternalServices.WeatherWebService;
using WeatherWebhook.Domain.Framework.Contracts.Response;
using WeatherWebhook.Domain.Models.Weather;
using WeatherWebhook.Domain.Repositories.Weather;
using Microsoft.Extensions.Logging;

namespace WeatherWebhook.Infrastructure.Models.Weather;

public class WeatherCommandModel : IWeatherCommandModel
{
    private readonly IWeatherCommandRepository _weatherCommandRepository;
    private readonly IWeatherInfoServiceClient _WeatherInfoServiceClient;
    private readonly ILogger _logger;

    public WeatherCommandModel(
        IWeatherCommandRepository weatherCommandRepository,
        IWeatherInfoServiceClient WeatherInfoServiceClient,
        ILogger<WeatherCommandModel> logger)
    {
        _weatherCommandRepository = weatherCommandRepository;
        _WeatherInfoServiceClient = WeatherInfoServiceClient;
        _logger = logger;
    }

    public async Task<DataResponse<WeatherInfoResultContract>> CreateAndGetWeatherInfoAsync(
        WeatherInfoCommand command, CancellationToken cancellationToken)
    {
        var weatherWebhookData = await _WeatherInfoServiceClient.GetLastInformationAsync(cancellationToken);

        if (weatherWebhookData is null)
        {
            Domain.Entities.Weather.Weather weather = await _weatherCommandRepository.GetLatestWeatherInfoAsync(cancellationToken);

            if (weather is null)
                return null;
            else
                return DataResponse<WeatherInfoResultContract>.Instance(
                    new WeatherInfoResultContract
                    {
                        Name = weather.Name,
                        Region = weather.Region,
                        Country = weather.Country,
                        Lat = weather.Lat,
                        Lon = weather.Lon
                    });
        }
        else
        {
            var weatherObj = new Domain.Entities.Weather.Weather(
                    weatherWebhookData.Name,
                    weatherWebhookData.Region,
                    weatherWebhookData.Country,
                    weatherWebhookData.Lat,
                    weatherWebhookData.Lon
            );

            #region
            // theWeather.Apply(new WeatherAdded
            // {
            //     Name = webhookData.Name,
            //     Region = webhookData.Region,
            //     Country = webhookData.Country,
            //     Lat = webhookData.Lat,
            //     Lon = webhookData.Lon
            // });
            #endregion

            await _weatherCommandRepository.AddAsync(weatherObj, cancellationToken);
            await _weatherCommandRepository.SaveChangesAsync(cancellationToken);

            return DataResponse<WeatherInfoResultContract>.Instance(
                    new WeatherInfoResultContract
                    {
                        Name = weatherWebhookData.Name,
                        Region = weatherWebhookData.Region,
                        Country = weatherWebhookData.Country,
                        Lat = weatherWebhookData.Lat,
                        Lon = weatherWebhookData.Lon
                    });
        }
    }
}