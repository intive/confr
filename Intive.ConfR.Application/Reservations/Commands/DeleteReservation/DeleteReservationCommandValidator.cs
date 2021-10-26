using FluentValidation;

namespace Intive.ConfR.Application.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommandValidator : AbstractValidator<DeleteReservationCommand>
    {
        public DeleteReservationCommandValidator()
        {
            RuleFor(r => r.Id).NotEmpty();
        }
    }
}
