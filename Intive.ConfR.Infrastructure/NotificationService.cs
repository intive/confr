using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Application.Notifications.Models;
using System.Threading.Tasks;

namespace Intive.ConfR.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}
