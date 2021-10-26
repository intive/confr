using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using MediatR;

namespace Intive.ConfR.Application.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRoomService _roomService;
        private readonly IReservationService _reservationService;
        private readonly IConfrHubService _hubService;

        public CreateReservationCommandHandler(IMapper mapper, IRoomService roomService, IReservationService reservationService, IConfrHubService hubService)
        {
            _mapper = mapper;
            _roomService = roomService;
            _reservationService = reservationService;
            _hubService = hubService;
        }

        public async Task<Unit> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var rooms = new List<Room>();

            foreach (var room in request.Rooms)
            {
                rooms.Add(await _roomService.GetRoomByEmail(room));
            }

            var scheduleRequest = _mapper.Map<CreateReservationCommand, ScheduleRequest>(request);
            
            var schedulesList = await _roomService.GetSchedulesList(scheduleRequest);

            if (_roomService.CheckRoomsAvailability(schedulesList))
            {
                throw new ConflictException("Room is already reserved for this date.");
            }

            var reservationRequest = _mapper.Map<List<Room>, ReservationRequest>(rooms);
             _mapper.Map<CreateReservationCommand, ReservationRequest>(request, reservationRequest);
            reservationRequest.Attendees = reservationRequest.Attendees.Concat(request.Attendees).ToList();
            
            var response = await _reservationService.CreateReservation(reservationRequest);

            //await _hubService.SendNewReservationToClient(response);

            return Unit.Value;
        }
    }
}
