namespace WeatherWebhook.Domain.Framework.Entities.Auditable;

public interface ICreatableEntity
{
    DateTime CreatedAt { get; set; }
}