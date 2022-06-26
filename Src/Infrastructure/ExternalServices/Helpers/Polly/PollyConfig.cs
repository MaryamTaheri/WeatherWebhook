using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;

namespace WeatherWebhook.Infrastructure.ExternalServices.Helpers.Polly.CircuitBreaker;

public static class PollyConfig
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions.HandleTransientHttpError()
        .WaitAndRetryAsync(new[]
        {
            TimeSpan.FromSeconds(2),
            TimeSpan.FromSeconds(3),
            TimeSpan.FromSeconds(4)
        }, (_, waitingTime) =>
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"polly retry policy {DateTime.Now.ToLongTimeString}");
            Console.ResetColor();
        });
    }

    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions.HandleTransientHttpError()
        // .AdvancedCircuitBreakerAsync(0.7, TimeSpan.FromSeconds(10), 2, TimeSpan.FromSeconds(20));
        .CircuitBreakerAsync(2, TimeSpan.FromSeconds(10));   //with console.log
    }
}