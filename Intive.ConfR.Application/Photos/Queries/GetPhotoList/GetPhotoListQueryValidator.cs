using FluentValidation;

namespace Intive.ConfR.Application.Photos.Queries.GetPhotoList
{
    public class GetPhotoListQueryValidator : AbstractValidator<GetPhotoListQuery>
    {
        public GetPhotoListQueryValidator()
        {
            RuleFor(query => query.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
