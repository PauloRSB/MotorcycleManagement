using FluentValidation;
using DocumentValidator;

namespace Challenge.Microservices.RiderApi.Commands.Validations
{
    public class RegisterRiderCommandValidator : AbstractValidator<RegisterRiderCommand>
    {
        public RegisterRiderCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Username is required.")
                .Length(5, 20).WithMessage("Username must be between 5 and 20 characters.");

            RuleFor(x => x.Cnpj)
                .NotEmpty().WithMessage("CNPJ is required.")
                .Must(BeAValidCNPJ).WithMessage("CNPJ is invalid.");

            RuleFor(x => x.Birthdate)
                .NotEmpty().WithMessage("Birthdate is required.")
                .LessThan(DateTime.Now).WithMessage("Birthdate must be in the past.");

            RuleFor(x => x.CnhNumber)
                .NotEmpty().WithMessage("CNH Number is required.")
                .Must(BeAValidCNH).WithMessage("CNH is invalid.");

            RuleFor(x => x.CnhType)
                .NotEmpty().WithMessage("CNH Type is required.")
                .Must(type => type?.ToString() is "A" or "B" or "AB")
                    .WithMessage("CNH Type must be 'A', 'B', or 'AB'.");
        }

        private bool BeAValidCNPJ(string? cnpj) => CnpjValidation.Validate(cnpj);

        private bool BeAValidCNH(string? cnh) => CnhValidation.Validate(cnh);
    }
}
