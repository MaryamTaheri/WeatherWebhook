using WeatherWebhook.Domain.Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WeatherWebhook.Infrastructure.Contexts.Common.Extension;

public static partial class Extensions
{
    public static void MapAuditableColumns(this EntityTypeBuilder modelBuilder)
    {
        modelBuilder.MapCreatableColumns();
    }

    public static void MapCreatableColumns(this EntityTypeBuilder modelBuilder)
    {
        modelBuilder.Property("CreatedAt")
            .HasColumnType("DateTime")
            .HasDefaultValue(DateTime.Now)
            .IsRequired();
    }

    public static void NeedToRegisterMappingConfig(this ModelBuilder modelBuilder)
    {
        var typesToRegister = AssemblyScanner.AllTypes("WeatherWebhook.Infrastructure", "(.*)")
            .Where(it =>
                !(it.IsAbstract || it.IsInterface)
                && it.GetInterfaces().Any(x =>
                    x.IsGenericType
                    && x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
            .ToList();

        foreach (var item in typesToRegister)
        {
            dynamic service = Activator.CreateInstance(item);

            modelBuilder.ApplyConfiguration(service);
        }
    }

    public static void NeedToRegisterEntitiesConfig<T>(this ModelBuilder modelBuilder)
    {
        var typesToRegister = AssemblyScanner.AllTypes("WeatherWebhook.Domain", "(.*)")
            .Where(it =>
                !(it.IsAbstract || it.IsInterface)
                && it.GetInterfaces().Any(x => x == typeof(T)))
            .ToList();

        foreach (var item in typesToRegister)
            modelBuilder.Entity(item);
    }
}