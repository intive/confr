using FluentValidation;
using Intive.ConfR.Application.Infrastructure.FluentValidator;
using System;

namespace Intive.ConfR.Application.Reservations.Commands.CreateRandomReservation
{
    public class CreateRandomReservationCommandValidator : AbstractValidator<CreateRandomReservationCommand>
    {
        public CreateRandomReservationCommandValidator()
        {
            RuleFor(r => r.From)
                .GreaterThan(DateTime.Now)
                .NotEmpty();
            RuleFor(r => r.To)
                .GreaterThan(r => r.From)
                .GreaterThan(DateTime.Now)
                .NotEmpty();
            RuleFor(r => r.Subject)
                .MaximumLength(255)
                .NotEmpty();
            RuleFor(r => r.Content).NotEmpty();
            RuleForEach(r => r.Attendees).SetValidator(new AttendeeValidator());
        }
    }
}
