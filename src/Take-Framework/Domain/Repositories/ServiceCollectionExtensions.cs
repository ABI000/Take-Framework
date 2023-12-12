using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.Domain.Repositories
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            var types = DependencyUtil.GetReferencedAssemblies().SelectMany(x => x.GetTypes().Where(w => !w.IsInterface && w.Name.EndsWith("Repository") && typeof(IBaseRepository).IsAssignableFrom(w)));
            foreach (var type in types)
            {
                services.AddScoped(type);
            }
            
            return services;
        }
    }
}