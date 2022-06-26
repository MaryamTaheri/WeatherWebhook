using System.Reflection;
using WeatherWebhook.Domain.Framework.Entities.Auditable;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WeatherWebhook.Infrastructure.Contexts;

public class AuditingInterceptor : ISaveChangesInterceptor
{
    public AuditingInterceptor()
    {
    }
    
    #region SavingChanges

    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        return result;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(result);
    }

    #endregion

    #region SavedChanges

    public int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        => result;

    public ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        => ValueTask.FromResult(result);

    #endregion

    #region SaveChangesFailed

    public void SaveChangesFailed(DbContextErrorEventData eventData)
    {
    }

    public async Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
        => await Task.CompletedTask;

    #endregion
}