using FluentValidation;

namespace Intive.ConfR.Application.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
    {
        public DeleteRoomCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
