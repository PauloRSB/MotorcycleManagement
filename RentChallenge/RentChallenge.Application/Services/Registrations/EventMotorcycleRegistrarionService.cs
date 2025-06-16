using RentChallenge.Application.DTOs.Requests;
using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Messaging;

namespace RentChallenge.Application.Services.Registrations
{
    public class EventMotorcycleRegistrationService(IMessagePublisher messagePublisher) : IEventMotorcycleRegistrationService
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        public async Task EnqueueRegistrationAsync(EventMotorcycleRegistered eventMotorcycle) =>
            await _messagePublisher.PublishAsync("register-motorcycle-event", eventMotorcycle);
    }
}
