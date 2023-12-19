using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.Domain.Uow
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.ScopedRegistrar(typeof(IUnitOfWork));
        }
    }
}