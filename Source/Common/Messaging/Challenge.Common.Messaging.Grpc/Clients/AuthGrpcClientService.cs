using Grpc.Net.Client;

namespace Challenge.Common.Messaging.Grpc.Clients
{
    public class AuthGrpcClientService
    {
        private readonly AuthService.AuthServiceClient _client;

        public AuthGrpcClientService(string authServerUrl)
        {
            var channel = GrpcChannel.ForAddress(authServerUrl);
            _client = new AuthService.AuthServiceClient(channel);
        }

        public async Task<CreateGroupResponse> CreateGroupAsync(CreateGroupRequest request)
        {
            return await _client.CreateGroupAsync(request);
        }

        public async Task<DisableUserResponse> DisableUserAsync(DisableUserRequest request)
        {
            return await _client.DisableUserAsync(request);
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            return await _client.LoginAsync(request);
        }

        public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request)
        {
            return await _client.RegisterUserAsync(request);
        }
    }
}
