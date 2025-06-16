using AutoMapper;
using RentChallenge.Application.DTOs.Requests;
using RentChallenge.Application.Interfaces.Handlers;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Repositories;
using RentChallenge.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Application.Services.Handlers
{
    public class EventMotorcycleHandlerService(IEventMotorcycleRegisteredRepository motorcycleRegisteredRepository, IUnitOfWork unitOfWork, IMapper mapper) : IEventMotorcycleHandlerService
    {
        private readonly IEventMotorcycleRegisteredRepository _motorcycleRegisteredRepository = motorcycleRegisteredRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task HandleAsync(EventMotorcycleRegistered eventMotorcycle)
        {
            await _motorcycleRegisteredRepository.AddAsync(eventMotorcycle);
            await _unitOfWork.CommitAsync();

        }
    }
}
