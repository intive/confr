using MediatR;

namespace Intive.ConfR.Application.Reservations.Commands.CancelReservation
{
    public class CancelReservationCommand : IRequest
    {
        public string Id { get; set; }
        public string Comment { get;  set; }
    }
}
