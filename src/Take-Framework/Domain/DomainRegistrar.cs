using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework;

public static class DomainRegistrar
{

    public static IServiceCollection ScopedRegistrar(this IServiceCollection services, params Type[] objects)
    {
        var types = DependencyUtil.GetReferencedAssemblies()
                      .SelectMany(x => x.GetTypes().Where(w => objects.Any(q => q.IsAssignableFrom(w)) && w.IsClass && !w.IsAbstract));
        foreach (var type in types)
        {
            var exposedService = type.GetInterface(DependencyUtil.GetInterfaceName(type.Name));

            if (exposedService is null)
            {
                services.AddScoped(type);
            }
            else
            {
                services.AddScoped(exposedService, type);
            }
        }
        return services;
    }

}
