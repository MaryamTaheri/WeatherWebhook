using WeatherWebhook.Domain.Framework.ValueObjects;

namespace WeatherWebhook.Domain.Framework.Entities.Contracts.AggregateRoots;

public abstract class GuidAuditableAggregateRoot : AuditableAggregateRoot<EntityUuid>
{
}