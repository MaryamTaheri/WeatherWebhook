using WeatherWebhook.Domain.Framework.ValueObjects;
using WeatherWebhook.Infrastructure.Contexts.Common.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WeatherWebhook.Infrastructure.Contexts.FluentApi.SqlServer.Weather;

public class WeatherConfig : IEntityTypeConfiguration<Domain.Entities.Weather.Weather>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Weather.Weather> builder)
    {
        builder.ToTable("weather", "dbo");

        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v));

        builder.Property(c => c.Name)
        .HasColumnType("VARCHAR(100)")
        .HasMaxLength(100);

         builder.Property(c => c.Region)
        .HasColumnType("VARCHAR(100)")
        .HasMaxLength(100);

        builder.Property(c => c.Country)
        .HasColumnType("VARCHAR(100)")
        .HasMaxLength(100);

        builder.Property(c => c.Lat)
            .HasColumnType("float")
            .IsRequired(false);

        builder.Property(c => c.Lon)
            .HasColumnType("float")
            .IsRequired(false);

        builder.MapAuditableColumns();
    }
}