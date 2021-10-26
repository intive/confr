using FluentValidation;
using FluentValidation.Validators;

namespace Intive.ConfR.Application.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
