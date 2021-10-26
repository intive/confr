using Intive.ConfR.Application.Notifications.Models;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Intive.ConfR.Infrastructure
{
    public class EmailService
    {
        public static Task SendAsync(Mail message)
        {
            var fromAddress = new MailAddress(message.From, message.FromName);
            var fromPassword = message.FromPassword;

            var toAddress = new MailAddress(message.To, message.ToName);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            smtp.Send(
                new MailMessage(fromAddress, toAddress)
                {
                    Subject = message.Subject,
                    Body = message.Body
                });

            return Task.CompletedTask;
        }
    }
}
