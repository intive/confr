using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Application.Reservations.Queries.GetReservation;
using Intive.ConfR.Domain.Entities;
using MediatR;

namespace Intive.ConfR.Application.Reservations.Queries.GetAllReservations
{
    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, GetAllReservationsModel>
    {
        private readonly IMapper _mapper;
        private readonly IReservationService _reservationService;

        public GetAllReservationsQueryHandler(IMapper mapper, IReservationService reservationService)
        {
            _mapper = mapper;
            _reservationService = reservationService;
        }

        public async Task<GetAllReservationsModel> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var responseDetail = await _reservationService.GetAllReservations(request.Mail, request.Start.ToString("s"), request.Count);

            var response = _mapper.Map<List<GraphReservation>, List<GetReservationModel>>(responseDetail);

            return new GetAllReservationsModel()
            {
                Value = response
            };
        }
    }
}
