using FluentValidation;

namespace Intive.ConfR.Application.Reservations.Commands.CancelReservation
{
    public class CancelReservationCommandValidator : AbstractValidator<CancelReservationCommand>
    {
        public CancelReservationCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty();
            RuleFor(r => r.Comment)
                .MaximumLength(255)
                .NotEmpty();
        }
    }
}
