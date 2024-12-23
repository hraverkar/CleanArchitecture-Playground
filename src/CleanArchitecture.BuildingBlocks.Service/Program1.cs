using Autofac;
using Autofac.Extensions.DependencyInjection;
using CleanArchitecture.Application.AutofacModules;
using CleanArchitecture.BuildingBlocks.EventBus;
using CleanArchitecture.BuildingBlocks.Services.App;
using CleanArchitecture.BuildingBlocks.Services.Core;
using CleanArchitecture.BuildingBlocks.Services.Core.Configuration;
using CleanArchitecture.BuildingBlocks.Services.Core.IntegrationEventHandlers;
using CleanArchitecture.Core.IntegrationEvents;
using CleanArchitecture.Infrastructure.AutofacModules;
using CleanArchitecture.Infrastructure.Settings;
using MediatR;
using Microsoft.Azure.Amqp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

public class Program1
{
    public static readonly string Namespace = typeof(Program1).Namespace;
    public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

    public static async Task Main(string[] args)
    {
        var host = BuildingHostBuilder(args);
        await host.RunAsync();

    }

    private static IHost BuildingHostBuilder(string[] args)
    {
        return (IHost)new HostBuilder()
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureHostConfiguration(configHost =>
            {
                configHost.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables().AddCommandLine(args);
            }).ConfigureServices((hostContext, services) =>
            {
                var eventTypes = GetEventTypes(hostContext.Configuration);
                var appSettings = hostContext.Configuration.GetSection("ServiceBus").Get<DatabaseSettings>();
                services.Configure<DatabaseSettings>(hostContext.Configuration.GetSection("ServiceBus"));
                services.AddHttpClient();
                services.AddMemoryCache();

                RegisterEventListeners(services, hostContext.Configuration);

                services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(TaxAutomationEventHandler).GetTypeInfo().Assembly));
                //services.AddMediatR(p =>
                //{

                //    p.AsScoped();
                //}, typeof(TaxAutomationEvent).GetTypeInfo().Assembly);
            })
            .ConfigureContainer<ContainerBuilder>((hostContext, builder) =>
            {
                builder.RegisterModule(new ApplicationModule());
                builder.RegisterModule(new InfrastructureModule(hostContext.Configuration));
                builder.RegisterModule(new ServicesModule());
            }).UseConsoleLifetime()
            .Build();
    }

    private static void RegisterEventListeners(IServiceCollection services, IConfiguration configuration)
    {
        var eventTypes = GetEventTypes(configuration);

        var allEventTypes = Assembly.GetAssembly(typeof(TaxAutomationEvent))
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IntegrationEvent)) && !t.IsAbstract)
            .ToList();

        foreach (var type in eventTypes)
        {
            var eventType = allEventTypes.FirstOrDefault(t => string.Equals(t.Name, type, StringComparison.CurrentCultureIgnoreCase));
            if (eventType == null)
            {
                throw new InvalidOperationException($"{type} is not a valid event type");
            }

            var eventServiceType = typeof(EventService<>);
            var genericEventType = eventServiceType.MakeGenericType(eventType);

            var method = typeof(ServiceCollectionHostedServiceExtensions).GetMethods()
                    .Where(m => m.Name == "AddHostedService" && m.GetParameters().Count() == 1).First();
            var genericMethod = method.MakeGenericMethod(genericEventType);
            object[] parameters = { services };
            genericMethod.Invoke(null, parameters);
        }
    }

    private static List<string> GetEventTypes(IConfiguration configuration)
    {
        var t = new List<string>
        {    "TaxAutomationEvent"
        };
        return t;
        //return configuration.GetSection("Events").Get<List<string>>() ?? new List<string>();
    }
}
