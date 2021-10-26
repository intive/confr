using FluentValidation;

namespace Intive.ConfR.Application.Photos.Queries.GetThumbnailList
{
    class GetThumbnailListQueryValidator : AbstractValidator<GetThumbnailListQuery>
    {
        public GetThumbnailListQueryValidator()
        {
            RuleFor(query => query.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
