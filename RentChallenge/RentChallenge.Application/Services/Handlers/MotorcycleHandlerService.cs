using AutoMapper;
using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Application.Interfaces.Handlers;
using RentChallenge.Application.Mappers;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Repositories;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RentChallenge.Application.Services.Handlers
{
    public class MotorcycleHandlerService(IMotorcycleRepository motorcycleRepository, IUnitOfWork unitOfWork, IMapper mapper) : IMotorcycleHandlerService
    {
        private readonly IMotorcycleRepository _motorcycleRepository = motorcycleRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task HandleAsync(RegisterMotorcycleRequestDTO motorcycle)
        {
            await _motorcycleRepository.AddAsync(_mapper.Map<Motorcycle>(motorcycle));
            await _unitOfWork.CommitAsync();

        }

    }
}
