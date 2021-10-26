using System.Collections.Generic;
using Intive.ConfR.Application.Photos.Models;
using MediatR;

namespace Intive.ConfR.Application.Photos.Queries.GetPhotoList
{
    public class GetPhotoListQuery : IRequest<List<RoomPhotoDto>>
    {
        public string Email { get; set; }
        public bool RequireSas { get; set; }
    }
}
