using FluentValidation;

namespace Intive.ConfR.Application.Photos.Commands.DeletePhoto
{
    public class DeletePhotoCommandValidator : AbstractValidator<DeletePhotoCommand>
    {
        public DeletePhotoCommandValidator()
        {
            RuleFor(command => command.RoomEmail)
                .NotEmpty()
                .EmailAddress();
            RuleFor(command => command.PhotoName).NotEmpty();
        }
    }
}
