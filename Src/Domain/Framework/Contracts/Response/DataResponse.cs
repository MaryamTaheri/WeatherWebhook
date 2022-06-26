using System.Net;

namespace WeatherWebhook.Domain.Framework.Contracts.Response;

public class DataResponse<TData>
{
    public string Level { get; protected init; } = "INFO";
    public string Key { get; protected init; } = "SUCCESS";
    public string Title { get; protected init; }
    public string Description { get; protected init; }
    public HttpStatusCode HttpCode { get; protected init; } = HttpStatusCode.OK;
    public int SituationCode { get; protected init; }
    public TData Data { get; protected init; }

    public static DataResponse<TData> Instance(TData data, string title = null, string description = null, string traceId = null, int situationCode = 0,
        HttpStatusCode httpCode = HttpStatusCode.OK, string key = "SUCCESS", string level = "INFO")
        => new()
        {
            Level = level,
            Key = key,
            Data = data,
            Title = title,
            Description = description,
            HttpCode = httpCode,
            SituationCode = situationCode
        };
}