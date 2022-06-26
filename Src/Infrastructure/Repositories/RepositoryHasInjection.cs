using WeatherWebhook.Domain.Framework.Services;
using WeatherWebhook.Domain.Repositories.Weather;
using WeatherWebhook.Infrastructure.Repositories.Weather;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherWebhook.Infrastructure.Repositories;

public class RepositoryHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        //Commands:
        collection.AddScoped<IWeatherCommandRepository, WeatherCommandRepository>();
    }
}