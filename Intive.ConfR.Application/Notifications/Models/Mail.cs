namespace Intive.ConfR.Application.Notifications.Models
{
    public class Mail : Message
    {
        public string FromPassword { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
    }
}
