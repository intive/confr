using AutoMapper;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Intive.ConfR.Application.Rooms.Queries.GetRoomAvailability
{
    public class GetRoomAvailabilityQueryHandler : IRequestHandler<GetRoomAvailabilityQuery, RoomAvailabilityViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IRoomService _roomService;

        public GetRoomAvailabilityQueryHandler(IMapper mapper, IRoomService roomService)
        {
            _mapper = mapper;
            _roomService = roomService;
        }

        public async Task<RoomAvailabilityViewModel> Handle(GetRoomAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var scheduleRequest = _mapper.Map<GetRoomAvailabilityQuery, ScheduleRequest>(request);

            var schedulesList = await _roomService.GetSchedulesList(scheduleRequest);

            var isReserved = _roomService.CheckRoomsAvailability(schedulesList);
            var availabilityString = schedulesList[0].AvailabilityView;

            return new RoomAvailabilityViewModel()
            {
                IsReserved = isReserved,
                AvailabilityString = availabilityString
            };
        }
    }
}
