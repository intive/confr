using FluentValidation;

namespace Intive.ConfR.Application.Photos.Commands.UpdatePhoto
{
    public class UpdatePhotoCommandValidator : AbstractValidator<UpdatePhotoCommand>
    {
        public UpdatePhotoCommandValidator()
        {
            RuleFor(command => command.RoomEmail)
                .NotEmpty()
                .EmailAddress();
            RuleFor(command => command.PhotoName).NotEmpty();
            RuleFor(command => command.Photo).NotEmpty();
            RuleFor(command => command.ThumbnailHeight).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(command => command.ThumbnailWidth).NotNull().GreaterThanOrEqualTo(0);
        }
    }
}
