using WeatherWebhook.Application.Commands.Weather;
using WeatherWebhook.Domain.Application.Commands.Weather;
using WeatherWebhook.Domain.Framework.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherWebhook.Application.Commands;

public class ApplicationCommandHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddScoped<IWeatherCommands, WeatherCommands>();
    }
}