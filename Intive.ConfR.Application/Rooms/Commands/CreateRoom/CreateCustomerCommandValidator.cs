using FluentValidation;

namespace Intive.ConfR.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
