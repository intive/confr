using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;
using MediatR;

namespace Intive.ConfR.Application.Reservations.Commands.CancelReservation
{
    public class CancelReservationComandHandler : IRequestHandler<CancelReservationCommand>
    {
        private readonly IReservationService _reservationService;

        public CancelReservationComandHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<Unit> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            await _reservationService.CancelReservation(request.Id, request.Comment);

            return Unit.Value;
        }
    }
}
