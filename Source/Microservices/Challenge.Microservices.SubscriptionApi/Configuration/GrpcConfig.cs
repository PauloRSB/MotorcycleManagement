using Challenge.Common.Messaging.Grpc;
using Challenge.Microservices.SubscriptionApi.Infra.Services;

namespace Challenge.Microservices.SubscriptionApi.Configuration
{
    /// <summary>
    /// Configuration for gRPC clients
    /// </summary>
    public static class GrpcConfig
    {
        /// <summary>
        /// Configures gRPC clients
        /// </summary>
        public static IServiceCollection SetupGrpc(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure gRPC client for RiderService
            services.AddGrpcClient<RiderService.RiderServiceClient>(options =>
            {
                var riderApiUrl = configuration["Services:RiderApi:GrpcUrl"]
                    ?? "http://rider:8080";

                options.Address = new Uri(riderApiUrl);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                // For development with self-signed certificates
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                return handler;
            });

            // Register the service that uses gRPC
            services.AddScoped<IRiderValidationService, RiderValidationService>();

            return services;
        }
    }
}