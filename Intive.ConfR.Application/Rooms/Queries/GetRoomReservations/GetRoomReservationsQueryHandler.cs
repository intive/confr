using AutoMapper;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Intive.ConfR.Application.Rooms.Queries.GetRoomReservations
{
    public class GetRoomReservationsQueryHandler : IRequestHandler<GetRoomReservationsQuery, RoomReservationsViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IReservationService _reservationService;

        public GetRoomReservationsQueryHandler(IMapper mapper, IReservationService reservationService)
        {
            _mapper = mapper;
            _reservationService = reservationService;
        }

        public async Task<RoomReservationsViewModel> Handle(GetRoomReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservationsList = await _reservationService.GetReservationsList(request.From, request.To, request.Email);
            var userReservations = await _reservationService.GetReservationsList(request.From, request.To);

            var specificUserReservations = userReservations
                .Where(ur =>ur.Attendees.Any(r => r.EmailAddress.Address == request.Email 
                                                  && r.Status.Response == "accepted")).ToList();

            var reservations = specificUserReservations.Any()
                ? _reservationService.SwapReservations(specificUserReservations, reservationsList)
                : reservationsList;

            return new RoomReservationsViewModel()
            {
                Reservations = _mapper.Map<List<GraphReservation>, List<Reservation>>(reservations)
            };
        }
    }
}
