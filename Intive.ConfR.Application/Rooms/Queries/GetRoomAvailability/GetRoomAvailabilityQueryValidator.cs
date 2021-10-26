using FluentValidation;

namespace Intive.ConfR.Application.Rooms.Queries.GetRoomAvailability
{
    public class GetRoomAvailabilityQueryValidator : AbstractValidator<GetRoomAvailabilityQuery>
    {
        public GetRoomAvailabilityQueryValidator()
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
