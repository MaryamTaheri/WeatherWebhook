using System.Data;
using WeatherWebhook.Domain.Framework.Entities.Auditable;
using WeatherWebhook.Domain.Framework.Repositories;
using WeatherWebhook.Domain.Framework.ValueObjects;
using WeatherWebhook.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace WeatherWebhook.Infrastructure.Repositories;

public class CommandRepository<TEntity, TKey> : ICommandRepository<TEntity, TKey>
    where TEntity : class, IAuditableEntity, ICreatableEntity
{
    protected readonly DbContext Context;

    public DbSet<TEntity> Entities { get; }

    public CommandRepository(DbContextBase context)
    {
        Context = context;
        Entities = context.Set<TEntity>();
    }

    public async Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken)
    {
        return await Entities.FindAsync(EntityUuid.FromString(id.ToString()), cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Entities.AddAsync(entity, cancellationToken);
    }

    public void Modify(TEntity entity)
    {
        Entities.Update(entity);
    }

    public async Task<IDbContextTransaction> StartTransAsync(IsolationLevel level = IsolationLevel.ReadUncommitted, CancellationToken cancellationToken = default)
        => await Context.Database.BeginTransactionAsync(level, cancellationToken);

    public void SaveChange()
    {
        Context.SaveChanges();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await Context.SaveChangesAsync(cancellationToken);
    }
}