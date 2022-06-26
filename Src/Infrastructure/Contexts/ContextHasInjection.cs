using WeatherWebhook.Domain.Framework.Services;
using WeatherWebhook.Infrastructure.Contexts.Providers;
using WeatherWebhook.Infrastructure.Contexts.Providers.Sqlite;
using WeatherWebhook.Infrastructure.Contexts.Providers.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherWebhook.Infrastructure.Contexts;

public class ContextHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        var context = "FAKE";
        #region
        // if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CURRENT_CONTEXT")))
        //     context = Environment.GetEnvironmentVariable("CURRENT_CONTEXT");
        #endregion

        switch (context)
        {
            case "ORIGINAL":

                collection.AddDbContext<SqlServerQueryDbContext>();
                collection.AddScoped<QueryDbContext, SqlServerQueryDbContext>();

                collection.AddDbContext<SqlServerCommandDbContext>();
                collection.AddScoped<CommandDbContext, SqlServerCommandDbContext>();

                break;

            case "FAKE":

                //Query .....

                //Command
                collection.AddDbContext<SqliteCommandDbContext>();
                collection.AddScoped<CommandDbContext, SqliteCommandDbContext>();
                break;
        }
    }
}