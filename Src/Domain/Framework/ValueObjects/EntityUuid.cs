using WeatherWebhook.Domain.Framework.Exceptions;

namespace WeatherWebhook.Domain.Framework.ValueObjects;

public sealed record EntityUuid(Guid Value) : ValueObject
{
    public static EntityUuid Generate() => FromGuid(Guid.NewGuid());

    public static EntityUuid FromGuid(Guid uuid)
    {
        return new EntityUuid(uuid);
    }

    public static EntityUuid FromString(string uuid)
    {
        var value = Guid.Parse(uuid);
        return new EntityUuid(value);
    }
}