using MediatR;

namespace Intive.ConfR.Application.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommand : IRequest
    {
        public string Id { get; set; }
    }
}
