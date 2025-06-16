using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RentChallenge.Application;
using RentChallenge.Consumer;
using RentChallenge.Infrastructure;
using RentChallenge.Infrastructure.Data;

namespace RentChallenge.API
{
    public class Configuration
    {
        public static void ResolveDepedencies(WebApplicationBuilder builder)
        {

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddConsumer();
            builder.Services.AddApplication();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RentChallenge API", Version = "v1" });
            });

        }
    }
}