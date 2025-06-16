using RentChallenge.Application.Interfaces.Handlers;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Application.Services.Handlers
{
    public class RentalHandlerService(IRentalRepository repository) : IRentalHandlerService
    {
        private readonly IRentalRepository _repository = repository;
        public async Task HandleAsync(Rental rental) =>
            await _repository.AddAsync(rental);
    }
}
