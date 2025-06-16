using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Messaging;
using System.Threading.Tasks;

namespace RentChallenge.Application.Services.Registrations
{
    public class RentalRegistrationService(IMessagePublisher messagePublisher) : IRentalRegistrationService
    {
        private readonly IMessagePublisher _messagePublisher = messagePublisher;

        public async Task EnqueueRegistrationAsync(Rental rental) =>
            await _messagePublisher.PublishAsync("register-rental", rental);
        
    }
}
