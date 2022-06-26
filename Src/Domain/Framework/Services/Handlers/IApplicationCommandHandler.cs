using MediatR;

namespace WeatherWebhook.Domain.Framework.Services.Handlers;

public interface IApplicationCommandHandler<in TRequest, TResult> : IRequestHandler<TRequest, TResult>
    where TRequest : IApplicationCommand<TResult>
{

}