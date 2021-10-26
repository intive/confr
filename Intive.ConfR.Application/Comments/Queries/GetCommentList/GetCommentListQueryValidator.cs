using FluentValidation;

namespace Intive.ConfR.Application.Comments.Queries.GetCommentList
{
    public class GetCommentListQueryValidator : AbstractValidator<GetCommentListQuery>
    {
        public GetCommentListQueryValidator()
        {
            RuleFor(c => c.RoomEmail)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
