using FluentValidation;

namespace Intive.ConfR.Application.Photos.Queries.GetThumbnail
{
    public class GetThumbnailQueryValidator : AbstractValidator<GetThumbnailQuery>
    {
        public GetThumbnailQueryValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
