using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace TakeFramework.Web.Authentication
{
    public static class TFAuthenticationExtensions
    {
        public static IServiceCollection AddTFAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = TFAuthenticationHandler.SchemeName;
                o.DefaultChallengeScheme = TFAuthenticationHandler.SchemeName;
                o.AddScheme<TFAuthenticationHandler>(TFAuthenticationHandler.SchemeName, TFAuthenticationHandler.SchemeName);
            });

            services.AddSingleton<IAuthenticationHandler, TFAuthenticationHandler>();
            return services;
        }
    }
}
