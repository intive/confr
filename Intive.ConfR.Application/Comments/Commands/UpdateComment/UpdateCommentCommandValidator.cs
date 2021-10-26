using FluentValidation;
using Intive.ConfR.Application.Infrastructure.FluentValidator;

namespace Intive.ConfR.Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(command => command.CommentId)
                .NotEmpty()
                .Must(CustomValidator.ValidateGuid).WithMessage(c => $"Guid '{c.CommentId}' is invalid.");
            RuleFor(command => command.Body)
                .MaximumLength(255)
                .NotEmpty();
        }
    }
}
