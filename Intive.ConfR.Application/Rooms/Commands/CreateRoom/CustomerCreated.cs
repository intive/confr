using MediatR;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Application.Notifications.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Intive.ConfR.Application.Rooms.Commands.CreateRoom
{
    public class RoomCreated : INotification
    {
        public string RoomId { get; set; }

        public class RoomCreatedHandler : INotificationHandler<RoomCreated>
        {
            private readonly INotificationService _notification;

            public RoomCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(RoomCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new Message());
            }
        }
    }
}
