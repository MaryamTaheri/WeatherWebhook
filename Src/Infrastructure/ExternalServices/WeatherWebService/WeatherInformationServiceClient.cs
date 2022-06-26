using WeatherWebhook.Domain.ExternalServices.WeatherWebService;
using WeatherWebhook.Domain.ExternalServices.WeatherWebService.Dtos;
using WeatherWebhook.Domain.Framework.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WeatherWebhook.Infrastructure.ExternalServices.WeatherWebService;

public class WeatherInfoServiceClient : IWeatherInfoServiceClient
{
    private const string RequestUri = "8d58f999-1acc-4b64-988a-ad7d4e31d9fa";
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private HttpClient _connection;
    private readonly ILogger _logger;

    public WeatherInfoServiceClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<WeatherInfoServiceClient> logger)
    {
        _configuration = configuration.GetSection("Services");
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<WeatherInfoResponseDto> GetLastInformationAsync(CancellationToken cancellationToken)
    {
        try
        {
            initHttpRequest();

            var response = await _connection.GetAsync(RequestUri, cancellationToken);

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogInformation(responseContent);

            if (!response.IsSuccessStatusCode)
                throw new Dexception(Situation.Make(SitKeys.Unprocessable),
                    new List<KeyValuePair<string, string>>
                        {new(":Message:", "can not connect to webservice")});

            var result = JsonConvert.DeserializeObject<WeatherInfoResponseDto>(responseContent);           

            if (result is null)
                throw new Dexception(Situation.Make(SitKeys.Unprocessable),
                    new List<KeyValuePair<string, string>> { new(":Message:", "invalid data") });

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException.ToString());
        }

        return null;

        /////////////////////////
        void initHttpRequest()
        {
            _connection = _httpClientFactory.CreateClient("WeatherService");
            _connection.Timeout = TimeSpan.FromSeconds(5);
            _connection.BaseAddress = new Uri(_configuration["Weather:ApiUrl"]);
            _connection.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        }
    }
}