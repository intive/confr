using Intive.ConfR.Application.Photos.Models;
using MediatR;

namespace Intive.ConfR.Application.Photos.Queries.GetPhoto
{
    public class GetPhotoQuery : IRequest<RoomPhotoDto>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public bool RequireSas { get; set; }
    }
}
