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
    public class DeliveryManHandlerService(IDeliveryManRepository repository, IUnitOfWork unitOfWork) : IDeliveryManHandlerService
    {
        public readonly IDeliveryManRepository _repository = repository;
        public readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task HandleAsync(DeliveryMan deliveryMan)
        {
            if (deliveryMan.DateOfBirth.Kind == DateTimeKind.Unspecified)
                deliveryMan.DateOfBirth = DateTime.SpecifyKind(deliveryMan.DateOfBirth, DateTimeKind.Utc);

            await _repository.AddAsync(deliveryMan);
            await _unitOfWork.CommitAsync();
        }

    }
}
