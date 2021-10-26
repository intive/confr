using FluentValidation;
using Intive.ConfR.Application.Infrastructure.FluentValidator;

namespace Intive.ConfR.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(c => c.CommentId)
                .NotEmpty()
                .Must(CustomValidator.ValidateGuid).WithMessage(c => $"Guid '{c.CommentId}' is invalid.");
        }
    }
}
