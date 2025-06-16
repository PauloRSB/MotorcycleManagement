using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Messaging;

namespace RentChallenge.Application.Services.Registrations
{
    public class MotorcycleRegistrationService(IMessagePublisher messagePublisher) : IMotorcycleRegistrationService
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        public async Task EnqueueRegistrationAsync(RegisterMotorcycleRequestDTO motorcycle) =>
            await _messagePublisher.PublishAsync("register-motorcycle", motorcycle);
    }
}
