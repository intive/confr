using MediatR;

namespace Intive.ConfR.Application.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommand : IRequest
    {
        public string Id { get; set; }
    }
}
