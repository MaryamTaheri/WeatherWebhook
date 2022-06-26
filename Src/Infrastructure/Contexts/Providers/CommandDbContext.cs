using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace WeatherWebhook.Infrastructure.Contexts.Providers;

public class CommandDbContext : DbContextBase
{
    public CommandDbContext(DbContextOptions options)
        : base(options)
    {
        ConnectionStringName = "SqlWriteNode";
    }
}