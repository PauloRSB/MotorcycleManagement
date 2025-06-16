using FluentValidation;
using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RentChallenge.Application.Validadors.Motorcycle
{
    public class RegisterMotorcycleValidator : AbstractValidator<RegisterMotorcycleRequestDTO>
    {
        public RegisterMotorcycleValidator()
        {
            RuleFor(x => x.Identifier).NotEmpty();
            RuleFor(x => x.Year).InclusiveBetween(2000, DateTime.UtcNow.Year);
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.NumberPlate).NotEmpty()
                                       .Must((motorcycle) =>
                                       {
                                           var clean = Regex.Replace(motorcycle ?? string.Empty, "[^a-zA-Z0-9]", "");
                                           return clean.Length == 7;
                                       });
        }
    }
}
