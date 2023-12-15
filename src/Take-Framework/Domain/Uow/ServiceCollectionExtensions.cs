using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using TakeFramework.Domain.Repositories;
using TakeFramework.Domain.Services;

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