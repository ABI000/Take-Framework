
using TakeFramework.Swagger;
using Sample.Host.Shared;
namespace Sample.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwagger(builder.Configuration);

            builder.Services.AddHostConfiguration(builder.Configuration);

            var app = builder.Build();

            app.UseHostConfiguration(builder.Configuration);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(builder.Configuration);
            }
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}