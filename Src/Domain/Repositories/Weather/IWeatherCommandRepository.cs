using WeatherWebhook.Domain.Framework.Repositories;

namespace WeatherWebhook.Domain.Repositories.Weather;

public interface IWeatherCommandRepository : ICommandRepository<Entities.Weather.Weather, Guid>
{
    Task<Entities.Weather.Weather> GetLatestWeatherInfoAsync(CancellationToken cancellationToken);
}