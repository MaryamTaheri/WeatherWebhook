using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherWebhook.Domain.Framework.Services;

public interface IHaveInjection
{
    void Inject(IServiceCollection collection, IConfiguration configuration);
}