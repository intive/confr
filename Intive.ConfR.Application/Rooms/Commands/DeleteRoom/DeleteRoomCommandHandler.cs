using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Domain.Entities;

namespace Intive.ConfR.Application.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand>
    {
        public DeleteRoomCommandHandler()
        { }

        public async Task<Unit> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}
