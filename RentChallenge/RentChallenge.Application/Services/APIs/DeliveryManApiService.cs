using AutoMapper;
using FluentValidation;
using RentChallenge.Application.DTOs.Requests.DeliveryMan;
using RentChallenge.Application.Interfaces.APIs;
using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Application.Services.Registrations;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Exceptions;
using RentChallenge.Domain.Interfaces.Repositories;
using RentChallenge.Domain.Interfaces.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Application.Services.APIs
{
    public class DeliveryManApiService(IValidator<RegisterDeliveryManDTO> validator, 
                                       IDeliveryManRegistrationService registrationService,
                                       IDeliveryManRepository repository,
                                       IUnitOfWork unitOfWork,
                                       IMapper _mapper,
                                       IFileStorageService fileStorageService) : IDeliveryManApiService
    {
        private readonly IDeliveryManRegistrationService _registrationService = registrationService;
        private readonly IValidator<RegisterDeliveryManDTO> _validator = validator;
        private readonly IDeliveryManRepository _repository = repository;
        private readonly IMapper _mapper = _mapper;
        private readonly IFileStorageService _fileStorageService = fileStorageService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task RegisterAsync(RegisterDeliveryManDTO deliveryMan)
        {
            var result = await _validator.ValidateAsync(deliveryMan);

            if (!result.IsValid || await DeliveryManExists(deliveryMan))
                throw new ValidationException("Dados inválidos.");

            await _registrationService.EnqueueRegistrationAsync(_mapper.Map<DeliveryMan>(deliveryMan));
        }

        public async Task UploadCnhImage(Stream stream, string deliveryManIdentifier, string contentType)
        {
            var deliveryMan = await _repository.GetByIdentifierAsync(deliveryManIdentifier);

            if (deliveryMan is null)
                throw new NotFoundException("Entregador não encontrado.");

            var filePath = await _fileStorageService.UploadAsync(stream, deliveryMan.CnhNumber, contentType);

            deliveryMan.CnhImageUrl = filePath;
            await _repository.UpdateAsync(deliveryMan);
            await _unitOfWork.CommitAsync();
        }



        private async Task<bool> DeliveryManExists(RegisterDeliveryManDTO deliveryMan)
        {
            var deliveryManDb = await _repository.GetByCnpjAsync(deliveryMan.Cnpj) ?? await _repository.GetByCnhAsync(deliveryMan.CnhNumber);
            return deliveryManDb is not null;
        }
    }
}
