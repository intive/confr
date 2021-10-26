using MediatR;

namespace Intive.ConfR.Application.Photos.Commands.DeletePhoto
{
    public class DeletePhotoCommand : IRequest
    {
        public string RoomEmail { get; set; }
        public string PhotoName { get; set; }
    }
}
