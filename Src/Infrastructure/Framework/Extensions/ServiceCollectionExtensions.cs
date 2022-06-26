using System.Reflection;
using WeatherWebhook.Domain.Framework.Exceptions;
using WeatherWebhook.Domain.Framework.Helpers;
using WeatherWebhook.Domain.Framework.Services;
using WeatherWebhook.Infrastructure.Framework.HttpRouting;
using WeatherWebhook.Infrastructure.Framework.Tools.MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WeatherWebhook.Infrastructure.Framework.Extensions;

public static class ServiceCollectionExtensions
{
    public static void DynamicInject(this IServiceCollection services, IConfiguration configuration, string nameSpace)
    {
        var typesToRegister = AssemblyScanner.AllTypes(nameSpace, "(Infrastructure)+")
            .Where(it => typeof(IHaveInjection).IsAssignableFrom(it))
            .ToList();
        typesToRegister.AddRange(AssemblyScanner.AllTypes(nameSpace, "(Application)+")
            .Where(it => typeof(IHaveInjection).IsAssignableFrom(it)));

        foreach (var item in typesToRegister)
        {
            var service = (IHaveInjection) Activator.CreateInstance(item);
            try
            {
                service?.Inject(services, configuration);
            }
            catch // (Exception exception)
            {
                // exception.Log();
            }
        }
    }

    /// <summary>
    /// Registers handlers and mediator types from the specified assemblies
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="assemblies">Assemblies to scan</param>
    /// <param name="configuration">The action used to configure the options</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services, IEnumerable<Assembly> assemblies, Action<MediatRServiceConfiguration> configuration)
    {
        var all = assemblies?.ToList() ?? new List<Assembly>();
        if (!all.Any())
            throw new Dexception(Situation.Make(SitKeys.Forbidden),
                new List<KeyValuePair<string, string>> {new(":پیام:", "امکان آماده‌سازی سیستم وجود ندارد.")});

        var serviceConfig = new MediatRServiceConfiguration();
        configuration?.Invoke(serviceConfig);
        MediatRServiceRegistrar.AddRequiredServices(services, serviceConfig);
        MediatRServiceRegistrar.AddMediatRClasses(services, all);

        return services;
    }

    public static IServiceCollection AddMediatR(this IServiceCollection services, string nameSpace)
    {
        var handlerAssemblyMarkerTypes = AssemblyScanner.AllTypes(nameSpace, "(.*)")
            .Where(it => !(it.IsAbstract || it.IsInterface));
        return services.AddMediatR(handlerAssemblyMarkerTypes, configuration: null);
    }

    /// <summary>
    /// Registers handlers and mediator types from the assemblies that contain the specified types
    /// </summary>
    /// <param name="services"></param>
    /// <param name="handlerAssemblyMarkerTypes"></param>
    /// <param name="configuration">The action used to configure the options</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services,
        IEnumerable<Type> handlerAssemblyMarkerTypes,
        Action<MediatRServiceConfiguration> configuration)
        => services.AddMediatR(handlerAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly), configuration);

    public static void AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out var methodInfo))
                {
                    return false;
                }

                if (methodInfo.DeclaringType == null) return false;
                var versions = methodInfo.DeclaringType
                    .GetCustomAttributes(true)
                    .OfType<ApiVersionAttribute>()
                    .SelectMany(a => a.Versions);

                return versions.Any(v => $"v{v.ToString()}" == docName);
            });
            options.SwaggerDoc("v1.0",
                new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "V1 API",
                    Description = "Space Travel WebApi",
                    TermsOfService = new Uri("http://alibaba.ir")
                });           
        });
    }

    public static void AddCustomApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(v =>
        {
            v.ReportApiVersions = true;
            v.AssumeDefaultVersionWhenUnspecified = true;
            v.DefaultApiVersion = new ApiVersion(1, 0);
        });
        services.AddVersionedApiExplorer(o =>
        {
            o.GroupNameFormat = "'v'VVV";
            o.SubstituteApiVersionInUrl = true;
        });
    }

    public static void AddCustomControllers(this IServiceCollection services, string nameSpace)
    {
        services
            .AddMediatR(nameSpace)
            .AddControllers(options => { options.Conventions.Add(new RouteTokenTransformerConvention(new SnakeCaseRouter())); })
            .AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; })
            .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();
    }
}