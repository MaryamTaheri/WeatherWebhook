using WeatherWebhook.Domain.Framework.Entities.Auditable;

namespace WeatherWebhook.Domain.Framework.Entities.Contracts.AggregateRoots;

public abstract class AuditableAggregateRoot<TKey> : AggregateRoot<TKey>, IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
}