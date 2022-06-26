using WeatherWebhook.Domain.Framework.Entities.Contracts.AggregateRoots;

namespace WeatherWebhook.Domain.Entities.Weather;

public partial class Weather : GuidAuditableAggregateRoot
{
    public string Name { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public double? Lat { get; set; }
    public double? Lon { get; set; }
}