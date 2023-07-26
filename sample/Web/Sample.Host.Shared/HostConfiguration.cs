using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TakeFramework.AutoMapper;
using TakeFramework.Cache;
using TakeFramework.Domain.Repositories;
using TakeFramework.Domain.Services;
using TakeFramework.Domain.Uow;
using TakeFramework.EntityFrameworkCore;
using TakeFramework.Json;
using TakeFramework.JWT;
using TakeFramework.Localization;
using TakeFramework.Web;
using TakeFramework.Web.Authentication;
using TakeFramework.Web.Authorization;
using TakeFramework.Web.Middleware;

namespace Sample.Host.Shared
{

    public static class HostConfiguration
    {
        public static IServiceCollection AddHostConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks();
            services.AddHttpContextAccessor();
            services.AddCache(configuration);
            services.AddAutoMapper();
            services.AddService();
            services.AddRepository();
            services.AddTakeFrameworkDbContext<SampleDbContext>(configuration);
            services.AddTakeFrameworkDbContext<Sample2DbContext>(configuration);
            services.AddErorrMiddleware();
            services.AddLocalization(configuration);
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());//日期时间
                options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
                options.JsonSerializerOptions.Converters.Add(new LongToStringConverter());
            });

            services.AddUnitOfWork();
            services.AddJwt(configuration);
            services.AddTFAuthentication();

            services.AddTFAuthorization();
            return services;
        }
        public static IApplicationBuilder UseHostConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseLocalization(configuration);
            app.UseErorrMiddleware();
            return app;
        }
    }
}
