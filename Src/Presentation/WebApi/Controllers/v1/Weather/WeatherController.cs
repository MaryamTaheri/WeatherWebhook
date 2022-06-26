using WeatherWebhook.Domain.Contracts.API.Weather;
using WeatherWebhook.Domain.Contracts.Commands.Weather;
using WeatherWebhook.Domain.Framework.Contracts.Response;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using static WeatherWebhook.Domain.Events.Globals;

namespace WeatherWebhook.Presentation.WebApi.Controllers.v1.Weather;

[ApiVersion("1.0")]
[Area(WeatherApi.WeatherArea)]
public class WeatherController : BaseController
{
    private readonly IMediator _mediator;

    public WeatherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(nameof(WeatherDetail))]
    public async Task<ActionResult<DataResponse<WeatherInfoResultContract>>> WeatherDetail(CancellationToken cancellationToken)
        => Dynamic(await _mediator.Send(new WeatherInfoCommand(), cancellationToken));
}