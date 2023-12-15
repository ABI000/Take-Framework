using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.Web.Authorization
{
    public static class TFAuthorizationExtensions
    {
        public static IServiceCollection AddTFAuthorization(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(TFAuthorizationPolicyProvider.POLICY_PREFIX, policy => policy.AddRequirements(new TFPolicyRequirement()));
            services.AddSingleton<IAuthorizationHandler, TFAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, TFAuthorizationPolicyProvider>();
            return services;
        }
    }
}
