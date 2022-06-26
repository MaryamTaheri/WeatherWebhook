using WeatherWebhook.Domain.Framework.Helpers;
using WeatherWebhook.Infrastructure.Framework.Extensions;
using WeatherWebhook.Presentation.WebApi.Middlewares;

namespace WeatherWebhook.Presentation.WebApi;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        GlobalConfig.Config = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {        
        services.AddCustomControllers("WeatherWebhook");

        services.AddCors(options => options.AddPolicy("SpaceTravelCorsPolicy", builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

        services.AddCustomApiVersioning();
        services.AddCustomSwagger();

        services.DynamicInject(Configuration, "WeatherWebhook");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        
        app.UseCustomErrorHandler();

        app.UseCustomSwagger();

        if (env.IsProduction())
            app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseCustomLocalization();

        app.UseRouting();

        app.UseCustomCors();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });    

        ////
        app.EnsureSqliteDb();    
    }
}