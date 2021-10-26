using MediatR;

namespace Intive.ConfR.Application.Rooms.Queries.GetRoomDetail
{
    public class GetRoomDetailQuery : IRequest<GetRoomDetailModel>
    {
        public string Email { get; set; }
    }
}
