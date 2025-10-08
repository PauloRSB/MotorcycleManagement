using AutoMapper;
using Challenge.Common.Core.Cqrs.Interfaces;
using Challenge.Common.Core.Response.Factories;
using Challenge.Common.Core.Response.Models.Interfaces;
using Challenge.Common.Core.Validation.Extensions;
using Challenge.Microservices.SubscriptionApi.Infra.Data.Entities;
using Challenge.Microservices.SubscriptionApi.Infra.Data.Repositories;
using Challenge.Microservices.SubscriptionApi.Infra.Services;
using FluentValidation;

namespace Challenge.Microservices.SubscriptionApi.Commands.Handlers
{
    /// <summary>
    /// Handles subscription creation
    /// </summary>
    public class CreateSubscriptionCommandHandler(
        IMapper mapper,
        ISubscriptionRepository repository,
        IValidator<CreateSubscriptionCommand> validator,
        ILogger<CreateSubscriptionCommandHandler> logger,
        IRiderValidationService riderValidationService) : ICommandHandler<CreateSubscriptionCommand>
    {
        public async Task<IResponse> Handle(CreateSubscriptionCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Validation failed for create subscription. Errors: {Errors}",
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                return ResponseFactory.CreateBadRequestResponse(validationResult.ToErrorDictionary());
            }

            // Validate rider has category A license
            var isValidRider = await riderValidationService.HasCategoryALicenseAsync(command.RiderIdentifier!);
            if (!isValidRider)
            {
                var errors = ValidationExtensions.SingleError(
                    "RiderId",
                    "Only riders with category 'A' or 'AB' license can rent motorbikes");
                return ResponseFactory.CreateBadRequestResponse(errors);
            }

            // Check if motorbike is available
            var hasActiveSubscription = await repository.HasActiveSubscriptionAsync(command.MotorbikeIdentifier!);
            if (hasActiveSubscription)
            {
                var errors = ValidationExtensions.SingleError(
                    "MotorbikeId",
                    "Motorbike is already rented");
                return ResponseFactory.CreateConflictResponse(errors);
            }

            try
            {
                var subscription = mapper.Map<Subscription>(command);

                await repository.AddAsync(subscription);

                logger.LogInformation(
                    "Subscription created successfully. Identifier: {Identifier}",
                    command.Identifier);

                return ResponseFactory.CreateCreatedResponse(subscription);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating subscription");
                return ResponseFactory.CreateCriticalResponse(ex);
            }
        }
    }
}