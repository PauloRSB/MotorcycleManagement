using RentChallenge.Application.DTOs.Requests.DeliveryMan;
using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Messaging;
using System.Threading.Tasks;

namespace RentChallenge.Application.Services.Registrations
{
    public class DeliveryManRegistrationService(IMessagePublisher messagePublisher) : IDeliveryManRegistrationService
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        public async Task EnqueueRegistrationAsync(DeliveryMan deliveryMan) =>
            await _messagePublisher.PublishAsync("register-deliveryman", deliveryMan);
    }
}
