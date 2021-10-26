using FluentValidation;

namespace Intive.ConfR.Application.Rooms.Queries.GetRoomReservations
{
    public class GetRoomReservationsQueryValidator : AbstractValidator<GetRoomReservationsQuery>
    {
        public GetRoomReservationsQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.From)
                .NotEmpty();

            RuleFor(x => x.To)
                .NotEmpty()
                .GreaterThan(x => x.From);
        }
    }
}
