using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Intive.ConfR.Application.Interfaces;
using System.Linq;

namespace Intive.ConfR.Application.Rooms.Queries.GetRoomDetail
{
    public class GetRoomDetailQueryHandler : IRequestHandler<GetRoomDetailQuery, GetRoomDetailModel>
    {
        private readonly IRoomService _roomService;

        public GetRoomDetailQueryHandler(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<GetRoomDetailModel> Handle(GetRoomDetailQuery request, CancellationToken cancellationToken)
        {
            var room = await _roomService.GetRoomByEmail(request.Email);

            return GetRoomDetailModel.Create(room);
        }
    }
}
