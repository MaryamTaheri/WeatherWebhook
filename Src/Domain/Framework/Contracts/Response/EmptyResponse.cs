using System.Net;

namespace WeatherWebhook.Domain.Framework.Contracts.Response;

public class EmptyResponse
{
    public string Level { get; protected init; }
    public string Title { get; protected init; }
    public string Key { get; protected init; }
    public string Description { get; protected init; }
    public HttpStatusCode HttpCode { get; protected init; }
    public int SituationCode { get; protected init; }

    public static EmptyResponse Instance(string title = null, string description = null, int situationCode = 0, 
        HttpStatusCode httpCode = HttpStatusCode.OK, string key = "SUCCESS", string level = "INFO")
        => new()
        {
            Level = level,
            Key = key,
            Title = title,
            Description = description,
            HttpCode = httpCode,
            SituationCode = situationCode
        };
}