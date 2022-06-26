using WeatherWebhook.Domain.ExternalServices.WeatherWebService;
using WeatherWebhook.Domain.Framework.Services;
using WeatherWebhook.Infrastructure.ExternalServices.WeatherWebService;
using WeatherWebhook.Infrastructure.ExternalServices.Helpers.Polly.CircuitBreaker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherWebhook.Infrastructure.ExternalServices;

public class ExternalServiceHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddHttpClient<WeatherInfoServiceClient>("WeatherService")
        .AddPolicyHandler(PollyConfig.GetRetryPolicy())
        .AddPolicyHandler(PollyConfig.GetCircuitBreakerPolicy());

        collection.AddScoped<IWeatherInfoServiceClient, WeatherInfoServiceClient>();
    }
}