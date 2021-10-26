using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Intive.ConfR.Application.Interfaces;
using Intive.ConfR.Domain.Entities;

namespace Intive.ConfR.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommand : IRequest
    {
        public string Id { get; set; }

        public class Handler : IRequestHandler<CreateRoomCommand, Unit>
        {
            private readonly INotificationService _notificationService;
            private readonly IMediator _mediator;

            public Handler(
                INotificationService notificationService,
                IMediator mediator)
            {
                _notificationService = notificationService;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
            {
                return Unit.Value;
            }
        }
    }
}
