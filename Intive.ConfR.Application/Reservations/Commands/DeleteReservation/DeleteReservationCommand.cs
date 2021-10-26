using MediatR;

namespace Intive.ConfR.Application.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommand : IRequest
    {
        public string Id { get; set; }
    }
}
