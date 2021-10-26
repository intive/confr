using Intive.ConfR.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;

namespace Intive.ConfR.Application.Reservations.Commands.CreateRandomReservation
{
    public class CreateRandomReservationCommand : IRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<Attendee> Attendees { get; set; }
    }
}
