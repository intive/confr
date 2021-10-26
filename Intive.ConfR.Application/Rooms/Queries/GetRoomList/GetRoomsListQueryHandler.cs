using AutoMapper;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Intive.ConfR.Application.Rooms.Queries.GetRoomsList
{
    public class GetRoomsListQueryHandler : IRequestHandler<GetRoomsListQuery, RoomsListViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IRoomService _roomService;

        public GetRoomsListQueryHandler(IMapper mapper, IRoomService roomService)
        {
            _mapper = mapper;
            _roomService = roomService;
        }

        public async Task<RoomsListViewModel> Handle(GetRoomsListQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _roomService.GetRoomsBasicList();

            return new RoomsListViewModel
            {
                Rooms = _mapper.Map<IList<Room>, IList<RoomLookupModel>>(rooms)
            };
        }
    }
}
