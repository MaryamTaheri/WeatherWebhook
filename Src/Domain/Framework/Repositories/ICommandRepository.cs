using WeatherWebhook.Domain.Framework.Entities.Auditable;

namespace WeatherWebhook.Domain.Framework.Repositories;

public interface ICommandRepository<TEntity, TKey> : ICreatableCommandRepository<TEntity, TKey> 
    where TEntity : class, IAuditableEntity
{
    void Modify(TEntity entity);
}