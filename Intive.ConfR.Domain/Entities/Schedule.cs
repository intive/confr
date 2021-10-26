using System;
using System.Collections.Generic;

namespace Intive.ConfR.Domain.Entities
{
    public class Schedule
    {
        public string ScheduleId { get; set; }
        public string AvailabilityView { get; set; }
        public List<ScheduleItem> ScheduleItems { get; set; }
        public WorkingHours WorkingHours { get; set; }
        public FreeBusyError Error { get; set; }
    }

    public class FreeBusyError
    {
        public string Message { get; set; }
        public string ResponseCode { get; set; }
    }

    public class WorkingHours
    {
        public List<string> DaysOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeZoneBase TimeZone { get; set; }
    }

    public class TimeZoneBase
    {
        public string Name { get; set; }
    }

    public class ScheduleItem
    {
        public bool IsPrivate { get; set; }
        public string Status { get; set; }
        public string Subject { get; set; }
        public string Location { get; set; }
        public DateTimeTimeZone Start { get; set; }
        public DateTimeTimeZone End { get; set; }
    }
}
