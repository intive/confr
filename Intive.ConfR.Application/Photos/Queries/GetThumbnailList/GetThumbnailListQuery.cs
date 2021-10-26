using System.Collections.Generic;
using Intive.ConfR.Application.Photos.Models;
using MediatR;

namespace Intive.ConfR.Application.Photos.Queries.GetThumbnailList
{
    public class GetThumbnailListQuery : IRequest<List<RoomPhotoDto>>
    {
        public string Email { get; set; }
        public bool RequireSas { get; set; }
    }
}
