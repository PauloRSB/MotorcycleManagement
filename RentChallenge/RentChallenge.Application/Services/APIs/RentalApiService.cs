using FluentValidation;
using RentChallenge.Application.Interfaces.APIs;
using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Application.Services.APIs
{
    public class RentalApiService(IValidator<Rental> validator, IRentalRegistrationService registrationService) : IRentalApiService
    {
        private readonly IValidator<Rental> _validator = validator;
        private readonly IRentalRegistrationService _registrationService = registrationService;

        public async Task RegisterAsync(Rental rental)
        {
            var result = await _validator.ValidateAsync(rental);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            await _registrationService.EnqueueRegistrationAsync(rental);
        }
    }
}
