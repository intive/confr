using MediatR;
using Microsoft.AspNetCore.Http;

namespace Intive.ConfR.Application.Photos.Commands.CreatePhoto
{
    public class CreatePhotoCommand : IRequest<string>
    {
        public string RoomEmail { get; set; }
        public IFormFile Photo { get; set; }
        public int ThumbnailWidth { get; set; }
        public int ThumbnailHeight { get; set; }
    }
}
