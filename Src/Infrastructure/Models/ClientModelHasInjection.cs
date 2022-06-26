using WeatherWebhook.Domain.Framework.Services;
using WeatherWebhook.Domain.Models.Weather;
using WeatherWebhook.Infrastructure.Models.Weather;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherWebhook.Infrastructure.Models;

public class ClientModelHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        //Queries:

        //Commands:
        collection.AddScoped<IWeatherCommandModel, WeatherCommandModel>();
    }
}