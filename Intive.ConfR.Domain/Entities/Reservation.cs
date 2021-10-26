using System;
using Intive.ConfR.Domain.ValueObjects;

namespace Intive.ConfR.Domain.Entities
{
    public class Reservation
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Owner { get; set; }

        public static implicit operator Reservation(ScheduleItem scheduleItem)
        {
            var reservation = new Reservation();

            reservation.Subject = scheduleItem.Subject;
            reservation.StartTime = scheduleItem.Start.DateTime;
            reservation.EndTime = scheduleItem.End.DateTime;
            return reservation;
        }
    }
}
