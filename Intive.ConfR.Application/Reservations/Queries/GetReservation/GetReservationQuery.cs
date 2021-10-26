using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Intive.ConfR.Application.Reservations.Queries.GetReservation
{
    public class GetReservationQuery : IRequest<GetReservationModel>
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Mail { get; set; }
    }
}
