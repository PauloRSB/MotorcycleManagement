using AutoMapper;
using Challenge.Common.Core.Response.Models.Interfaces;
using Challenge.Common.Core.Validation.Extensions;
using FluentValidation;
using Challenge.Common.Core.Response.Factories;
using Challenge.Microservices.RiderApi.Infra.Data.Entities;
using Challenge.Microservices.RiderApi.Infra.Data.Repositories;

namespace Challenge.Microservices.RiderApi.Commands.Handlers
{
    /// <summary>
    /// Handles the registration of a new rider in the system
    /// </summary>
    public class RegisterRiderCommandHandler(
    IMapper mapper,
    IRiderRepository riderRepository,
    IValidator<RegisterRiderCommand> validator,
    ILogger<RegisterRiderCommandHandler> logger)
    : ICommandHandler<RegisterRiderCommand>
    {
        /// <summary>
        /// Processes the rider registration command
        /// </summary>
        /// <param name="command">The registration command containing rider information</param>
        /// <returns>A response indicating success or failure of the operation</returns>
        public async Task<IResponse> Handle(RegisterRiderCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                logger.LogWarning(
                    "Validation failed for register rider. Errors: {Errors}",
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

                return ResponseFactory.CreateBadRequestResponse(
                    validationResult.ToErrorDictionary());
            }

            try
            {
                var rider = mapper.Map<Rider>(command);
                rider.Active = true;

                var riderId = await riderRepository.AddAsync(rider);
                rider.Id = riderId;

                logger.LogInformation(
                    "Rider registered successfully. RiderId: {RiderId}, Cnpj: {Cnpj}",
                    riderId, rider.Cnpj);

                return ResponseFactory.CreateCreatedResponse(rider);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "Error registering rider. Cnpj: {Cnpj}",
                    command.Cnpj);

                return ResponseFactory.CreateCriticalResponse(ex);
            }
        }
    }
}
