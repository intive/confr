using FluentValidation;

namespace Intive.ConfR.Application.Photos.Queries.GetPhoto
{
    public class GetPhotoQueryValidator : AbstractValidator<GetPhotoQuery>
    {
        public GetPhotoQueryValidator()
        {
            RuleFor(query => query.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(query => query.Name).NotEmpty();
            RuleFor(query => query.RequireSas).NotNull();
        }
    }
}
