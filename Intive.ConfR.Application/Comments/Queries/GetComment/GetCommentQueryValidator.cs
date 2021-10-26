using FluentValidation;
using Intive.ConfR.Application.Infrastructure.FluentValidator;

namespace Intive.ConfR.Application.Comments.Queries.GetComment
{
    public class GetCommentQueryValidator : AbstractValidator<GetCommentQuery>
    {
        public GetCommentQueryValidator()
        {
            RuleFor(c => c.CommentId)
                .NotEmpty()
                .Must(CustomValidator.ValidateGuid).WithMessage(c => $"Guid '{c.CommentId}' is invalid.");
        }
    }
}
