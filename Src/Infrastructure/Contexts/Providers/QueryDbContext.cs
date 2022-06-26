using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace WeatherWebhook.Infrastructure.Contexts.Providers;

public class QueryDbContext : DbContextBase
{
    public QueryDbContext(DbContextOptions options, IHttpContextAccessor accessor)
        : base(options)
    {
        ConnectionStringName = "SqlReadNode";
    }
}