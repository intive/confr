using FluentValidation;

namespace Intive.ConfR.Application.Photos.Commands.CreatePhoto
{
    public class CreatePhotoCommandValidator : AbstractValidator<CreatePhotoCommand>
    {
        public CreatePhotoCommandValidator()
        {
            RuleFor(photo => photo.RoomEmail)
                .NotEmpty()
                .EmailAddress();
            RuleFor(photo => photo.Photo).NotEmpty();
            RuleFor(photo => photo.ThumbnailHeight)
                .NotNull()
                .GreaterThanOrEqualTo(0);
            RuleFor(photo => photo.ThumbnailWidth)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}
