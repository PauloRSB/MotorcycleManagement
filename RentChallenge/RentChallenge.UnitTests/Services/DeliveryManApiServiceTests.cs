using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using RentChallenge.Application.DTOs.Requests.DeliveryMan;
using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Application.Services.APIs;
using RentChallenge.Domain.Interfaces.Repositories;
using RentChallenge.Domain.Interfaces.Storage;
using System.Threading.Tasks;
using Xunit;

public class DeliveryManApiServiceTests
{
    [Fact]
    public async Task RegisterAsync_InvalidData_ThrowsValidationException()
    {
        // Arrange
        var validator = new Mock<IValidator<RegisterDeliveryManDTO>>();
        validator.Setup(v => v.ValidateAsync(It.IsAny<RegisterDeliveryManDTO>(), default))
                 .ReturnsAsync(new ValidationResult { });

        var registrationService = new Mock<IDeliveryManRegistrationService>();
        var repository = new Mock<IDeliveryManRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var mapper = new Mock<IMapper>();
        var fileStorage = new Mock<IFileStorageService>();

        var service = new DeliveryManApiService(
            validator.Object, registrationService.Object, repository.Object, unitOfWork.Object, mapper.Object, fileStorage.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            service.RegisterAsync(new RegisterDeliveryManDTO()));
    }
}
