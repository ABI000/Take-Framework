using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.Domain.Uow
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            var types = DependencyUtil.GetReferencedAssemblies().SelectMany(x => x.GetTypes().Where(w => !w.IsInterface && typeof(IUnitOfWork).IsAssignableFrom(w)));
            foreach (var type in types)
            {
                services.AddScoped(typeof(IUnitOfWork),type);
            }
            return services;
        }
    }
}