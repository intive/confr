using MediatR;
using Microsoft.AspNetCore.Http;

namespace Intive.ConfR.Application.Photos.Commands.UpdatePhoto
{
    public class UpdatePhotoCommand : IRequest
    {
        public string RoomEmail { get; set; }
        public string PhotoName { get; set; }
        public IFormFile Photo { get; set; }
        public int ThumbnailWidth { get; set; }
        public int ThumbnailHeight { get; set; }
    }
}
