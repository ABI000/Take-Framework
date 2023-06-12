using TakeFramework.Mapper;
using TakeFramework.Domain.Repositories;
using TakeFramework.Domain.Services;
using TakeFramework.EntityFrameworkCore;

namespace SampleWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            builder.Services.Configure<DBSettings>(builder.Configuration.GetSection(DBSettings.Position));
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper();


            builder.Services.AddService();
            builder.Services.AddRepository();
            builder.Services.AddTakeFrameworkDbContext<SampleDbContext>();



            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}