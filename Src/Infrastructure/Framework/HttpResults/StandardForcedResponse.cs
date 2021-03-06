using WeatherWebhook.Domain.Framework.Contracts.Response;
using WeatherWebhook.Infrastructure.Framework.HttpResults.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WeatherWebhook.Infrastructure.Framework.HttpResults;

[DefaultStatusCode(DefaultStatusCode)]
public class StandardForcedResponse : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status200OK;

    public StandardForcedResponse(EmptyResponse response) : base(new StandardForcedResponseContract
    {
        Result = new StandardForcedResultContract
        {
            Level = response.Level,
            Title = response.Key,
            Message = response.Title,
            Description = response.Description,
            Status = response.SituationCode,
        }
    })
        => StatusCode = (int) response.HttpCode;
}

[DefaultStatusCode(DefaultStatusCode)]
public class StandardForcedResponse<TData> : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status200OK;

    public StandardForcedResponse(DataResponse<TData> response) : base(new StandardForcedResponseContract<TData>
    {
        Data = response.Data,
        Result = new StandardForcedResultContract
        {
            Level = response.Level,
            Title = response.Key,
            Message = response.Title,
            Description = response.Description,
            Status = response.SituationCode,
        }
    })
        => StatusCode = (int) response.HttpCode;
}