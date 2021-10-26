using Intive.ConfR.Application.Notifications.Models;
using System.Threading.Tasks;

namespace Intive.ConfR.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
