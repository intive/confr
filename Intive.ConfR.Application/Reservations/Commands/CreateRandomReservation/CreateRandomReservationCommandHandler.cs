using AutoMapper;
using Intive.ConfR.Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Intive.ConfR.Domain.Entities;
using Intive.ConfR.Application.Exceptions;
using System;

namespace Intive.ConfR.Application.Reservations.Commands.CreateRandomReservation
{
    public class CreateRandomReservationCommandHandler : IRequestHandler<CreateRandomReservationCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRoomService _roomService;
        private readonly IReservationService _reservationService;
        private readonly IConfrHubService _hubService;

        public CreateRandomReservationCommandHandler(IMapper mapper, IRoomService roomService, IReservationService reservationService, IConfrHubService hubService)
        {
            _mapper = mapper;
            _roomService = roomService;
            _reservationService = reservationService;
            _hubService = hubService;
        }

        public async Task<Unit> Handle(CreateRandomReservationCommand request, CancellationToken cancellationToken)
        {
            var allRooms = await _roomService.GetRooms();

            var scheduleRequest = _mapper.Map<CreateRandomReservationCommand, ScheduleRequest>(request);
            scheduleRequest.Schedules = allRooms.Select(r => r.Email.ToString()).ToList();

            var scheduleList = await _roomService.GetSchedulesList(scheduleRequest);

            var freeRoom = _roomService.PickRandomSchedule(scheduleList);

            if (!freeRoom.Any())
            {
                throw new ConflictException("There is no available room at this date");
            }

            var room = new List<Room>
            {
                await _roomService.GetRoomByEmail(freeRoom[0].ScheduleId.ToString())
            };

            var reservationRequest = _mapper.Map<List<Room>, ReservationRequest>(room);
            _mapper.Map<CreateRandomReservationCommand, ReservationRequest>(request, reservationRequest);
            reservationRequest.Attendees = reservationRequest.Attendees.Concat(request.Attendees).ToList();

            var response = await _reservationService.CreateReservation(reservationRequest);

            //await _hubService.SendNewReservationToClient(response);

            return Unit.Value;
        }
    }
}
