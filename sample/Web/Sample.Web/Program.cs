
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Sample.Host.Shared;
using Serilog;
using Serilog.Events;
using TakeFramework.Swagger;

namespace Sample.Web
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
     .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
     .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
     .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
     .Enrich.FromLogContext()
     .WriteTo.Async(c => c.File("Logs/logs.txt"))
     .WriteTo.Async(c => c.Console())
     .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console());


            builder.Services.AddEndpointsApiExplorer();


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => builder.Configuration.Bind("JwtSettings", options));

            builder.Services.AddSwagger(builder.Configuration);
            builder.Services.AddMemoryCache();
            builder.Services.AddHostConfiguration(builder.Configuration);
            var app = builder.Build();

            app.UseSerilogRequestLogging();


            app.UseHostConfiguration(builder.Configuration);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(builder.Configuration);
            }
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

            return 0;
        }
    }
}