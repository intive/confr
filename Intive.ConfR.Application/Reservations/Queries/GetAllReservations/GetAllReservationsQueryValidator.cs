using FluentValidation;

namespace Intive.ConfR.Application.Reservations.Queries.GetAllReservations
{
    public class GetAllReservationsQueryValidator : AbstractValidator<GetAllReservationsQuery>
    {
        public GetAllReservationsQueryValidator()
        {
            RuleFor(r => r.Mail)
                .NotEmpty()
                .EmailAddress();
            RuleFor(r => r.Count).GreaterThan(0);
        }
    }
}
