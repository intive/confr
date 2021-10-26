using System;
using FluentValidation;
using Intive.ConfR.Application.Infrastructure.FluentValidator;

namespace Intive.ConfR.Application.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(r => r.Rooms).NotEmpty();
            RuleForEach(r => r.Rooms)
                .MaximumLength(50)
                .NotEmpty()
                .EmailAddress();
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
