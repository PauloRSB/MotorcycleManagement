using Challenge.Common.Messaging.Grpc;
using Grpc.Core;

namespace Challenge.Microservices.SubscriptionApi.Infra.Services
{

    /// <summary>
    /// Implementation using gRPC to communicate with RiderApi
    /// </summary>
    public class RiderValidationService(
        RiderService.RiderServiceClient grpcClient,
        ILogger<RiderValidationService> logger) : IRiderValidationService
    {
        public async Task<bool> HasCategoryALicenseAsync(string riderIdentifier)
        {
            try
            {
                var request = new ValidateRiderRequest
                {
                    RiderIdentifier = riderIdentifier
                };

                var response = await grpcClient.ValidateRiderCnhTypeAsync(
                    request,
                    deadline: DateTime.UtcNow.AddSeconds(5));

                if (!response.IsValid)
                {
                    logger.LogWarning(
                        "Rider validation failed. RiderIdentifier: {RiderIdentifier}, Message: {Message}",
                        riderIdentifier, response.Message);
                }

                return response.IsValid;
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.NotFound)
            {
                logger.LogWarning("Rider not found. RiderIdentifier: {RiderIdentifier}", riderIdentifier);
                return false;
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.DeadlineExceeded)
            {
                logger.LogError("Timeout validating rider. RiderIdentifier: {RiderIdentifier}", riderIdentifier);
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error validating rider. RiderIdentifier: {RiderIdentifier}", riderIdentifier);
                return false;
            }
        }
    }
}