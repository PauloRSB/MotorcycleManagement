using FluentValidation;
using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Application.Validadors
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(x => x.RentalPlanId).GreaterThan(0);
            RuleFor(x => x.MotorcycleId).GreaterThan(0);
            RuleFor(x => x.DeliveryManId).GreaterThan(0);
        }
    }
}
