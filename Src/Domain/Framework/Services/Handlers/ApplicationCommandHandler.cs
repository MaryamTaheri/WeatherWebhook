using MediatR;

namespace WeatherWebhook.Domain.Framework.Services.Handlers;

public abstract class ApplicationCommandHandler<TRequest> : AsyncRequestHandler<TRequest>
    where TRequest : IApplicationCommand
{
    public ApplicationCommandHandler()
    {
    }
}