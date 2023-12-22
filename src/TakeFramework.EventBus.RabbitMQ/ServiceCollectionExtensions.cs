using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.EventBus.RabbitMQ;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventBusRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PersistentConnectionOptions>(configuration.GetSection(PersistentConnectionOptions.Position));
        services.AddSingleton<IPersistentConnection, DefaultPersistentConnection>();
        return services.AddSingleton<IEventBus, EventBusRabbitMQ>();
    }
}
