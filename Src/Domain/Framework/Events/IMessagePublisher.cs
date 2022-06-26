namespace WeatherWebhook.Domain.Framework.Events;

public interface IMessagePublisher
{
    void Publish(BusEvent domainEvent);
}