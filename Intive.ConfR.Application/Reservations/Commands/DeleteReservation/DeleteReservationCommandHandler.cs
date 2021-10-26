using Intive.ConfR.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Intive.ConfR.Application.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand>
    {
        private readonly IReservationService _reservationService;

        public DeleteReservationCommandHandler(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<Unit> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            await _reservationService.DeleteReservation(request.Id);

            return Unit.Value;
        }
    }
}
