using Challenge.Common.Data.Mongo.Converters;
using Challenge.Microservices.RiderApi.Configuration;
using Challenge.Microservices.RiderApi.Configuration.Options;

namespace Challenge.Microservices.RiderApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services
                .SetupDatabase(builder.Configuration)
                .SetupDependencyInjection()
                .SetupApiVersioning()
                .SetupSwaggerGen()
                .SetupAws(builder.Configuration, builder.Environment);

            builder.Services.AddOptions<S3Options>()
                .BindConfiguration("AwsOptions:S3")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Logging.SetupLogging();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                    options.JsonSerializerOptions.Converters.Add(new ObjectIdJsonConverter());
                });

            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            app.SetupSwagger();

            app.MapControllers();

            app.Run();
        }
    }
}
