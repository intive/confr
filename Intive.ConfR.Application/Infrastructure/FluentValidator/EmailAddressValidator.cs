using FluentValidation;
using Intive.ConfR.Domain.Entities;

namespace Intive.ConfR.Application.Infrastructure.FluentValidator
{
    public class EmailAddressValidator : AbstractValidator<GraphEmailAddress>
    {
        public EmailAddressValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Address)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
