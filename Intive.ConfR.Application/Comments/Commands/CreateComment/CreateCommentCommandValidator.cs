using FluentValidation;

namespace Intive.ConfR.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(comment => comment.RoomEmail)
                .NotEmpty()
                .EmailAddress();
            RuleFor(comment => comment.Body)
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
