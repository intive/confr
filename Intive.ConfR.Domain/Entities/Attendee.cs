namespace Intive.ConfR.Domain.Entities
{
    public class Attendee
    {
        public string Type { get; set; }
        public ResponseStatus Status { get; set; }
        public GraphEmailAddress EmailAddress { get; set; }
    }
}
