using FluentValidation;
using Intive.ConfR.Domain.Entities;

namespace Intive.ConfR.Application.Infrastructure.FluentValidator
{
    public class AttendeeValidator : AbstractValidator<Attendee>
    {
        public AttendeeValidator()
        {
            RuleFor(a => a.EmailAddress)
                .NotEmpty()
                .SetValidator(new EmailAddressValidator());
            RuleFor(a => a.Type).NotEmpty();
        }
    }
}
