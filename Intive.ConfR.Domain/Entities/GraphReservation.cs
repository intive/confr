using System;
using System.Collections.Generic;

namespace Intive.ConfR.Domain.Entities
{
    public class GraphReservation
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset LastModifiedDateTime { get; set; }
        public string ChangeKey { get; set; }
        public List<string> Categories { get; set; }
        public string OriginalStartTimeZone { get; set; }
        public string OriginalEndTimeZone { get; set; }
        public string Uid { get; set; }
        public int ReminderMinutesBeforeStart { get; set; }
        public bool IsReminderOn { get; set; }
        public bool HasAttachments { get; set; }
        public string Subject { get; set; }
        public string BodyPreview { get; set; }
        public string Importance { get; set; }
        public string Sensitivity { get; set; }
        public bool IsAllDay { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsOrganizer { get; set; }
        public bool ResponseRequested { get; set; }
        public string SeriesMasterId { get; set; }
        public string ShowAs { get; set; }
        public string Type { get; set; }
        public string WebLink { get; set; }
        public string OnlineMeetingUrl { get; set; }
        public object Recurrence { get; set; } //object -> https://docs.microsoft.com/en-us/graph/api/resources/patternedrecurrence?view=graph-rest-beta
        public ResponseStatus ResponseStatus { get; set; }
        public ItemBody Body { get; set; }
        public DateTimeTimeZone Start { get; set; }
        public DateTimeTimeZone End { get; set; }
        public Location Location { get; set; }
        public List<Location> Locations { get; set; }
        public List<Attendee> Attendees { get; set; }
        public Organizer Organizer { get; set; }
    }
}
