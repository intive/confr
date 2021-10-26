using FluentValidation;

namespace Intive.ConfR.Application.Reservations.Queries.GetReservation
{
    public class GetReservationQueryValidator : AbstractValidator<GetReservationQuery>
    {
        public GetReservationQueryValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty();
            RuleFor(r => r.Mail)
                .NotEmpty()
                .EmailAddress();
        }   
    }
}
