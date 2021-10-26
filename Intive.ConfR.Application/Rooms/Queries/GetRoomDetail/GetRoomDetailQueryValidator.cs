using FluentValidation;

namespace Intive.ConfR.Application.Rooms.Queries.GetRoomDetail
{
    public class GetRoomDetailQueryValidator : AbstractValidator<GetRoomDetailQuery>
    {
        public GetRoomDetailQueryValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
