using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TakeFramework.IO;

namespace TakeFramework.Domain.Repositories
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            var fileInfos = FileUtilities.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Where(x => !x.Name.StartsWith("Microsoft.") && !x.Name.StartsWith("System."));
            var types = fileInfos.Select(x => Assembly.LoadFrom(x.FullName)).SelectMany(x => x.GetTypes().Where(w => !w.IsInterface && w.Name.EndsWith("Repository") && typeof(IBaseRepository).IsAssignableFrom(w)));
            foreach (var type in types)
            {
                services.AddScoped(type);
            }
            return services;
        }
    }
}