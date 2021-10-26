using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using MediatR;

namespace Intive.ConfR.Application.Reservations.Queries.GetReservation
{
    public class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, GetReservationModel>
    {
        private readonly IMapper _mapper;
        private readonly IReservationService _reservationService;

        public GetReservationQueryHandler(IMapper mapper, IReservationService reservationService)
        {
            _mapper = mapper;
            _reservationService = reservationService;
        }

        public async Task<GetReservationModel> Handle(GetReservationQuery request, CancellationToken cancellationToken)
        {
            var response = await _reservationService.GetReservation(request.Id, request.Mail);

            return _mapper.Map<GraphReservation, GetReservationModel>(response);
        }
    }
}

