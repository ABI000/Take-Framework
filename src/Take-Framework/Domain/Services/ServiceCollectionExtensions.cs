using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.Domain.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            var types = DependencyUtil.GetReferencedAssemblies().SelectMany(x => x.GetTypes().Where(w => !w.IsInterface
            && typeof(IBaseService).IsAssignableFrom(w)));
            foreach (var type in types)
            {
                services.AddScoped(type);
            }
            return services;
        }
    }
}