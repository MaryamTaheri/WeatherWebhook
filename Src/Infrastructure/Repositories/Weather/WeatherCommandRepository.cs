using WeatherWebhook.Domain.Repositories.Weather;
using WeatherWebhook.Infrastructure.Contexts.Providers;
using Microsoft.EntityFrameworkCore;

namespace WeatherWebhook.Infrastructure.Repositories.Weather;

public class WeatherCommandRepository : CommandRepository<Domain.Entities.Weather.Weather, Guid>, IWeatherCommandRepository
{
    public WeatherCommandRepository(CommandDbContext context) : base(context)
    {
    }

    public async Task<Domain.Entities.Weather.Weather> GetLatestWeatherInfoAsync(CancellationToken cancellationToken)
        => await Entities
            .OrderByDescending(w => w.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
}