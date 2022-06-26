using MediatR;

namespace WeatherWebhook.Domain.Framework.Services;

public interface IApplicationQuery<out TResult> : IRequest<TResult>
{
        
}